using System.Collections.Generic;
using System.Threading.Tasks;
using ArtHouse.Models;
namespace ArtHouse.Repository.Interfaces
{
    public interface IUserR
    {
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task AddUser(User user, string password);
        bool CheckUser(string email, string nickname);
        bool CheckUserPass(string email, string password);
        Task<User> GetUserById(int id);
        Task UpdateUserAsync(User user, int id);
        List<User> GetAllUser();
    }
}
