﻿using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("--> Consuming Bid Placed");

        var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);
        if (context.Message.BidStatus.Contains("Accepted") &&
            context.Message.Amount > auction.CurrentHighBid
        )
        {

        }
    }
}
