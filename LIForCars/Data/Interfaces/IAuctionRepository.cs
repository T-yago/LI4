using LIForCars.Models;

namespace LIForCars.Data.Interfaces
{
    public interface IAuctionRepository
    {
        IEnumerable<Auction> GetAll();
        Auction? GetById(int id);
        bool Create(Auction auction);
        bool Update(Auction auction);
        bool Delete(int id);
        bool UpdateAuthorizationStatus(int auctionId);
        bool CarIdExists(int carId);

        Task<Auction?> GetAuctionAsync(int idAuction);
        Task<(IEnumerable<Auction> auctions, int totalCount)> GetCurrentAuctionsAsync(int page, int pageSize, string orderBy, string filterBy);
        Task<(IEnumerable<Auction> auctions, int totalCount)> GetAuctionsUserAsync(int page, int pageSize, int idUser);
        Task<IEnumerable<Auction>> GetAuctionsUserWaitingApprovalAsync(int page, int pageSize, int idUser);
        Task<IEnumerable<Auction>> GetAllAuctionsUserAsync(int idUser);
        Task<(IEnumerable<Auction> auctions, int totalCount)> GetAllWaittingAuctionsAsync(int page, int pageSize);

    }
}