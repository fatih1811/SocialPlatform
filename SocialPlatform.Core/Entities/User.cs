using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  
        public string? Language { get; set; }  = "Türkçe";
        public DateTime? CreatedAt { get; set; } = DateTime.Now;  
        public DateTime? LastLoginAt { get; set; }  
        public bool? IsActive { get; set; } = true;  // Kullanıcı aktif mi? (silinmiş ya da devre dışı kullanıcılar için)
        public string? Role { get; set; } = "User";  // Kullanıcı rolü (Admin, User)
        public string? ProfilePictureUrl { get; set; } = "default";
    }
}
