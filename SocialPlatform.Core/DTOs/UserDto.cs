using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Core.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Language { get; set; } = "Türkçe";
        public string? ProfilePictureUrl { get; set; } = "default";
        public DateTime? LastLoginAt { get; set; }
    }

}
