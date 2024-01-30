using LIForCars.Models;

namespace LIForCars.Data.Interfaces
{
    public interface IUserRepository
    {
        bool SaveChanges();
        IEnumerable<User> GetAll();
        User? GetByUsername(string username);
        bool NifExists(int nif);
        bool CcExists(int cc);
        bool PhoneExists(int phone);
        bool UsernameExists(string username);
        bool EmailExists(string email);
        bool CheckPassword(string username, string password);
        bool Create(User newUser);
        bool Update(User newUser);
        bool Delete(User newUser);

        Task<User?> GetUserByIdAsync(int userId);
        Task<bool> IsAdminAsync(string username);
    }
}