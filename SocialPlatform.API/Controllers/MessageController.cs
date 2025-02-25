using Microsoft.AspNetCore.Mvc;
using SocialPlatform.Core.Entities;
using SocialPlatform.Service.IMessage;
using SocialPlatform.Core.DTOs;

namespace SocialPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null) return NotFound();
            return Ok(message);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMessagesByUser(int userId)
        {
            var messages = await _messageService.GetMessagesByUserIdAsync(userId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MessageRequestDto message)
        {
            await _messageService.AddAsync(message);
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MessageRequestDto message)
        {
            if (id != message.Id) return BadRequest();
            await _messageService.UpdateAsync(message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("unread/{userId}")]
        public async Task<IActionResult> GetUnreadMessages(int userId)
        {
            var messages = await _messageService.GetUnreadMessagesByUserAsync(userId);
            return Ok(messages);
        }

        [HttpPost("mark-read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _messageService.MarkMessageAsReadAsync(id);
            return NoContent();
        }

        [HttpGet("translations/{userId}")]
        public async Task<IActionResult> GetMessagesWithTranslation(int userId)
        {
            var messages = await _messageService.GetMessagesWithTranslationAsync(userId);
            return Ok(messages);
        }

        [HttpDelete("user/{messageId}/{userId}")]
        public async Task<IActionResult> DeleteMessageByUser(int messageId, int userId)
        {
            await _messageService.DeleteMessageByUserAsync(messageId, userId);
            return NoContent();
        }

        [HttpGet("between/{userId1}/{userId2}")]
        public async Task<IActionResult> GetMessagesBetweenUsers(int userId1, int userId2)
        {
            var messages = await _messageService.GetMessagesBetweenUsersAsync(userId1, userId2);
            return Ok(messages);
        }

        // Belirli iki kullanıcı arasındaki mesajları getirme
        [HttpGet("{senderId}/{receiverId}")]
        public async Task<IActionResult> GetMessages(int senderId, int receiverId)
        {
            var messages = await _messageService.GetMessagesAsync(senderId, receiverId);
            return Ok(messages);
        }

        // Yeni mesaj gönderme
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto messageDto)
        {
            await _messageService.SendMessageAsync(messageDto);
            return Ok();
        }

    }
}
