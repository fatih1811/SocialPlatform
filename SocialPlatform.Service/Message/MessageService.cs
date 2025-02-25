using SocialPlatform.Data.IRepo;
using SocialPlatform.Service.IMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialPlatform.Core.Entities;
using SocialPlatform.Core.DTOs;
using AutoMapper;

namespace SocialPlatform.Service.Message
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;  // AutoMapper kullanımı için IMapper ekleniyor

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;  // Mapper'ı enjekte ediyoruz
        }

        public async Task<MessageResponseDto> GetByIdAsync(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            return _mapper.Map<MessageResponseDto>(message);  // Dönüştürme işlemi
        }

        public async Task<List<MessageResponseDto>> GetAllAsync()
        {
            var messages = await _messageRepository.GetAllAsync();
            return _mapper.Map<List<MessageResponseDto>>(messages);  // Dönüştürme işlemi
        }

        public async Task<List<MessageResponseDto>> GetMessagesByUserIdAsync(int userId)
        {
            var messages = await _messageRepository.GetMessagesByUserIdAsync(userId);
            return _mapper.Map<List<MessageResponseDto>>(messages);  // Dönüştürme işlemi
        }

        public async Task AddAsync(MessageRequestDto messageRequestDto)
        {
            // MessageRequestDto'dan Message entity'sine dönüşüm yapılıyor
            var message = _mapper.Map<SocialPlatform.Core.Entities.Message>(messageRequestDto);
            await _messageRepository.AddAsync(message);
        }

        public async Task UpdateAsync(MessageRequestDto messageRequestDto)
        {
            // MessageRequestDto'dan Message entity'sine dönüşüm yapılıyor
            var message = _mapper.Map<SocialPlatform.Core.Entities.Message>(messageRequestDto);
            await _messageRepository.Update(message);
        }
        public async Task DeleteAsync(int messageId)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);
            if (message != null)
            {
                await _messageRepository.Delete(message);
            }
        }

        public async Task<List<MessageResponseDto>> GetUnreadMessagesByUserAsync(int userId)
        {
            var messages = await _messageRepository.GetUnreadMessagesByUserAsync(userId);
            return _mapper.Map<List<MessageResponseDto>>(messages);  // Dönüştürme işlemi
        }

        public async Task MarkMessageAsReadAsync(int messageId) => await _messageRepository.MarkMessageAsReadAsync(messageId);

        public async Task<List<MessageResponseDto>> GetMessagesWithTranslationAsync(int userId)
        {
            var messages = await _messageRepository.GetMessagesWithTranslationAsync(userId);
            return _mapper.Map<List<MessageResponseDto>>(messages);  // Dönüştürme işlemi
        }

        public async Task DeleteMessageByUserAsync(int messageId, int userId) => await _messageRepository.DeleteMessageByUserAsync(messageId, userId);

        public async Task<List<MessageResponseDto>> GetMessagesBetweenUsersAsync(int userId1, int userId2)
        {
            var messages = await _messageRepository.GetMessagesBetweenUsersAsync(userId1, userId2);
            return _mapper.Map<List<MessageResponseDto>>(messages);  // Dönüştürme işlemi
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesAsync(int senderId, int receiverId)
        {
            var messages = await _messageRepository.GetMessagesAsync(senderId, receiverId);
            return messages.Select(m => new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                SentAt = m.SentAt,
                IsRead = m.IsRead,
                ReplyToMessageId = m.ReplyToMessageId
            });
        }

        public async Task SendMessageAsync(SendMessageDto messageDto)
        {
            var message = new SocialPlatform.Core.Entities.Message
            {
                SenderId = messageDto.SenderId,
                ReceiverId = messageDto.ReceiverId,
                Content = messageDto.Content,
                SentAt = messageDto.Timestamp,
                IsRead = false
            };
            await _messageRepository.SendMessageAsync(message);
        }
    }
}
