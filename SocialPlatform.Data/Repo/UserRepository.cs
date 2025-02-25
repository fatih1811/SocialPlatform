using Microsoft.EntityFrameworkCore;
using SocialPlatform.Core.Entities;
using SocialPlatform.Data.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialPlatformContext _context;

        public UserRepository(SocialPlatformContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
        }

        public async Task Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.Where(u=> u.Email == email).FirstOrDefaultAsync();
           
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task ActivateUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null) 
            {
                user.IsActive = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null) 
            {
                user.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePasswordAsync(int userId, string newPassword)
        {
            var user = await  _context.Users.FindAsync(userId);
            if(user != null)
            {
                user.Password = newPassword;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string query)
        {
            return await _context.Users
                .Where(u => u.FirstName.Contains(query) || u.LastName.Contains(query) || u.Email.Contains(query))
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetChattedUsersAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.MessagesReceived.Any(m => m.SenderId == userId) || u.MessagesSent.Any(m => m.ReceiverId == userId))
                .ToListAsync();
        }
    }
}
