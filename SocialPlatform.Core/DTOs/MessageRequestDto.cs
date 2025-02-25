using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Core.DTOs
{
    public class MessageRequestDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }  // Mesajı gönderen kullanıcı
        public int ReceiverId { get; set; }  // Mesajı alan kullanıcı
        public string Content { get; set; }  // Mesajın orijinal içeriği
        public DateTime SentAt { get; set; }  // Mesaj gönderim zamanı
        public bool IsRead { get; set; }  // Mesajın okunma durumu
        public bool DeletedBySender { get; set; }  // Gönderen mesajı silmiş mi?
        public bool DeletedByReceiver { get; set; }  // Alıcı mesajı silmiş mi?
        public int? ReplyToMessageId { get; set; }  // Yanıtlanan mesajın ID'si (nullable)
    }
}
