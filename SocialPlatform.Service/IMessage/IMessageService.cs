using SocialPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialPlatform.Core.DTOs;
using SocialPlatform.Core.Entities;

namespace SocialPlatform.Service.IMessage
{
    public interface IMessageService
    {
        // Bu metodlar MessageResponseDto döndürmelidir.
        Task<MessageResponseDto> GetByIdAsync(int id);
        Task<List<MessageResponseDto>> GetAllAsync();
        Task<List<MessageResponseDto>> GetMessagesByUserIdAsync(int userId);
        Task AddAsync(MessageRequestDto message);
        Task UpdateAsync(MessageRequestDto message);
        Task DeleteAsync(int messageId);
        Task<List<MessageResponseDto>> GetUnreadMessagesByUserAsync(int userId);
        Task MarkMessageAsReadAsync(int messageId);
        Task<List<MessageResponseDto>> GetMessagesWithTranslationAsync(int userId);
        Task DeleteMessageByUserAsync(int messageId, int userId);
        Task<List<MessageResponseDto>> GetMessagesBetweenUsersAsync(int userId1, int userId2);
        Task<IEnumerable<MessageDto>> GetMessagesAsync(int senderId, int receiverId);
        Task SendMessageAsync(SendMessageDto messageDto);
    }
}
