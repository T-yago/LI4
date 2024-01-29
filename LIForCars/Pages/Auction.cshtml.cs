using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LIForCars.Models;
using LIForCars.Data.Interfaces;
using System.Security.Claims;

public class AuctionModel : PageModel
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IBidRepository _bidRepository;

    public AuctionModel(IAuctionRepository auctionRepository, IBidRepository bidRepository)
    {
        _auctionRepository = auctionRepository;
        _bidRepository = bidRepository;
    }

    public IActionResult OnGetHeaderPartial() => Partial("Shared/Header");
    public IActionResult OnGetLoginPartial() => Partial("Shared/Login");
    public IActionResult OnGetRegisterPartial() => Partial("Shared/Register");

    public Auction? AuctionDetails { get; private set; }
    public Bid? CurrentBid { get; private set; }
    
    [BindProperty]
    public int auctionId { get; set; }

    [BindProperty]
    public decimal bidValue { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        AuctionDetails = _auctionRepository.GetById(id);
        CurrentBid = await _bidRepository.GetCurrentBidForAuctionAsync(id);

        return Page();
    }

    public async Task<IActionResult> OnPostPlaceBid() {
        AuctionDetails = _auctionRepository.GetById(auctionId);
        CurrentBid = await _bidRepository.GetCurrentBidForAuctionAsync(auctionId);

        if (AuctionDetails == null) return new JsonResult(new { success = false, errors = new List<string> { "Auction not found" } });
        decimal currBidValue = AuctionDetails.BasePrice;

        if (CurrentBid != null) currBidValue = CurrentBid.BidValue;

        if (bidValue < currBidValue + AuctionDetails.MinIncrement) {
            ModelState.AddModelError("BidValue", "Value must be at least " + (currBidValue + AuctionDetails.MinIncrement));
        }

        var modelErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        if (!ModelState.IsValid) return new JsonResult(new { success = false, errors = modelErrors });
        
        
        var UserNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (UserNameIdentifier == null) return new JsonResult(new { success = false, errors = new List<string> { "Unknown Error" } });
        var bid = new Bid {
            AuctionId = auctionId,
            UserId = int.Parse(UserNameIdentifier),
            BidValue = bidValue,
            bidTime = DateTime.Now
        };

        _bidRepository.Create(bid);
        
        return new JsonResult(new { success = true });
    }

}