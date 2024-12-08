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
    }
}
