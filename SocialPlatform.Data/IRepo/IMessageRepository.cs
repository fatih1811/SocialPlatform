using SocialPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Data.IRepo
{
    public interface IMessageRepository
    {
        Task<Message> GetByIdAsync(int id);
        Task<List<Message>> GetAllAsync();
        Task<List<Message>> GetMessagesByUserIdAsync(int userId);
        Task AddAsync(Message message);
        Task Update(Message message);
        Task Delete(Message message);
        Task<List<Message>> GetUnreadMessagesByUserAsync(int userId);
        Task MarkMessageAsReadAsync(int messageId);
        Task<List<Message>> GetMessagesWithTranslationAsync(int userId);
        Task DeleteMessageByUserAsync(int messageId, int userId);
        Task<List<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2);
        Task<IEnumerable<Message>> GetMessagesAsync(int senderId, int receiverId);
        Task SendMessageAsync(Message message);
    }
}
