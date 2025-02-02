using Domain.Services.UIServices.AuctionService;
using Microsoft.AspNetCore.SignalR;

public class AuctionHub : Hub
{
    private readonly IAuctionService _auctionService;

    public AuctionHub(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    public async Task UpdateBid(int itemId, double newBid)
    {
        try
        {
            // Try updating bid in database
            bool success = _auctionService.UpdateBid(itemId, newBid);

            if (success)
            {
                // Notify all clients of new bid
                await Clients.All.SendAsync("ReceiveBidUpdate", itemId, newBid);
            }
            else
            {
                // Send error message back to client (optional)
                await Clients.Caller.SendAsync("BidError", "Bid placement failed. Ensure the bid is higher than the current price.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateBid: {ex.Message}"); // Logs the error
            await Clients.Caller.SendAsync("BidError", "An error occurred while processing your bid.");
        }
    }
}
