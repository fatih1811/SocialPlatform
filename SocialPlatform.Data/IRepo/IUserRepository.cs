using SocialPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Data.IRepo
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task Update(User user);
        Task Delete(User user);
        Task<User> FindByEmailAsync(string email);
        Task ActivateUserAsync(int userId);
        Task DeactivateUserAsync(int userId);
        Task UpdatePasswordAsync(int userId, string newPassword);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> SearchUsersAsync(string query);
        Task<IEnumerable<User>> GetChattedUsersAsync(int userId);
    }
}
