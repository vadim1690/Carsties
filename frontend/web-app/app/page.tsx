import Listings from "./auctions/Listings";

export default function Home() {
  console.log("server Component");
  return (
    <div>
      <Listings />
    </div>
  );
}
