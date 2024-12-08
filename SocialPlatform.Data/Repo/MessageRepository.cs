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

        public async Task<List<Message>> GetUnreadMessagesByUserAsync(int userId)
        {
            return await _context.Messages
        .Where(m => (m.ReceiverId == userId && !m.IsRead))
        .ToListAsync();
        }

        public async Task MarkMessageAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if(message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetMessagesWithTranslationAsync(int userId)
        {
            return await _context.Messages
        .Where(m => m.SenderId == userId || m.ReceiverId == userId)
        .Select(m => new Message
        {
            Id = m.Id,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            Content = m.Content,
            TranslatedContent = m.TranslatedContent,
            SentAt = m.SentAt,
            IsRead = m.IsRead,
            DeletedBySender = m.DeletedBySender,
            DeletedByReceiver = m.DeletedByReceiver,
            ReplyToMessageId = m.ReplyToMessageId
        })
        .ToListAsync();
        }

        public async Task DeleteMessageByUserAsync(int messageId, int userId)
        {
            var message = await _context.Messages
       .Where(m => m.Id == messageId && (m.SenderId == userId || m.ReceiverId == userId))
       .FirstOrDefaultAsync();

            if (message != null)
            {
                if (message.SenderId == userId)
                    message.DeletedBySender = true;
                if (message.ReceiverId == userId)
                    message.DeletedByReceiver = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2)
        {
            return await _context.Messages
        .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                    (m.SenderId == userId2 && m.ReceiverId == userId1))
        .ToListAsync();
        }
    }
}
