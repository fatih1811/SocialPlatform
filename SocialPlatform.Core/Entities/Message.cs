using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Core.Entities
{
    public class Message
    {
        public int Id { get; set; }  // Primary Key
        public int SenderId { get; set; }  // Mesajı gönderen kullanıcı
        public int ReceiverId { get; set; }  // Mesajı alan kullanıcı
        public string Content { get; set; }  // Mesajın orijinal içeriği
        public string TranslatedContent { get; set; }  // Mesajın çevrilmiş içeriği
        public DateTime SentAt { get; set; } = DateTime.Now;  // Mesaj gönderim zamanı
        public bool IsRead { get; set; } = false;  // Mesajın okunma durumu
        public bool DeletedBySender { get; set; } = false;  // Gönderen mesajı silmiş mi?
        public bool DeletedByReceiver { get; set; } = false;  // Alıcı mesajı silmiş mi?
        public int? ReplyToMessageId { get; set; }  // Yanıtlanan mesajın ID'si (nullable)

        // Navigation Properties
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public Message ReplyToMessage { get; set; }  // Yanıtlanan mesaj
    }
}
