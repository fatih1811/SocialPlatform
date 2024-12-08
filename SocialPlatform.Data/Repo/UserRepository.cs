﻿using Microsoft.EntityFrameworkCore;
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
    }
}
