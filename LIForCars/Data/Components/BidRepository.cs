using Microsoft.EntityFrameworkCore;
using LIForCars.Data.Interfaces;
using LIForCars.Models;

namespace LIForCars.Data.Components
{
    public class BidRepository : IBidRepository
    {
        private readonly MyLIForCarsDBContext _context;
        
        public BidRepository(MyLIForCarsDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Bid> GetAll()
        {
            return _context.Bid.ToList();
        }

        public Bid? GetById(int id)
        {
            return _context.Bid.FirstOrDefault(b => b.Id == id);
        }

        public bool Create(Bid bid)
        {
            try {
                _context.Bid.Add(bid);
                _context.SaveChanges();
            } catch (Exception) {
                return false;
            }
            return true;
        }

        public bool Update(Bid bid)
        {
            try {
                _context.Bid.Update(bid);
                _context.SaveChanges();
            } catch (Exception) {
                return false;
            }
            return true;

        }

        public bool Delete(int id)
        {
            try 
            {
                var bid = _context.Bid.Find(id);
                if (bid != null)
                {
                    _context.Bid.Remove(bid);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<Bid?> GetCurrentBidForAuctionAsync(int auctionId)
        {
            return await _context.Bid
                                 .Where(b => b.AuctionId == auctionId)
                                 .OrderByDescending(b => b.BidValue)
                                 .FirstOrDefaultAsync();
        }
    }
}