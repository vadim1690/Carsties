using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;
[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        var query = DB.PagedSearch<Item, Item>();

        // filter by search terms
        if (string.IsNullOrEmpty(searchParams.SearchTerm) == false)
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        // Order by
        query = searchParams.OrderBy switch
        {
            "make" => query.Sort(x => x.Ascending(a => a.Make)),
            "new" => query.Sort(x => x.Ascending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd)),
        };

        // Filter by
        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(
                x => x.AuctionEnd < DateTime.UtcNow.AddHours(6) &&
                x.AuctionEnd > DateTime.UtcNow),

            _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow),
        };


        // Filter by Seller
        if (string.IsNullOrEmpty(searchParams.Seller) == false)
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }

        // Filter by Winner
        if (string.IsNullOrEmpty(searchParams.Winner) == false)
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }

        // pagination
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var result = await query.ExecuteAsync();
        return Ok(new
        {
            result = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}
