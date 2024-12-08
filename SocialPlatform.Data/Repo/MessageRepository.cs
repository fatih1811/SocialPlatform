using Microsoft.EntityFrameworkCore;
using SocialPlatform.Core.Entities;
using SocialPlatform.Data.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Data.Repo
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SocialPlatformContext _context;

        public MessageRepository(SocialPlatformContext context)
        {
            _context = context;
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<List<Message>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<List<Message>> GetMessagesByUserIdAsync(int userId)
        {
            return await _context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task Update(Message message)
        {
            _context.Messages.Update(message);
        }

        public async Task Delete(Message message)
        {
            _context.Messages.Remove(message);
        }
    }
}
