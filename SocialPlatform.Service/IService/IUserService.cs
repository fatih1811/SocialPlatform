using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialPlatform.Core.DTOs;
using SocialPlatform.Core.Entities;
using SocialPlatform.Data.IRepo;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Service
{
    public interface IUserService
    {
        Task<UserResponseDto> SignUpAsync(UserRequestDto userRequestDto);
        Task<UserResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<UserResponseDto> UpdatePasswordAsync(int userId, string newPassword);
        Task ActivateUserAsync(int userId);
        Task DeactivateUserAsync(int userId);
        Task<IEnumerable<UserDto>> SearchUsersAsync(string query);
        Task<IEnumerable<UserDto>> GetChattedUsersAsync(int userId);
    }

}