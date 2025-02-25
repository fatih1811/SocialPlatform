using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialPlatform.Core.DTOs;
using SocialPlatform.Core.Entities;
using SocialPlatform.Core.Validator;
using SocialPlatform.Data.IRepo;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserResponseDto> SignUpAsync(UserRequestDto userRequestDto)
        {
            // Validation işlemi
            var validator = new UserRequestDtoValidator();
            var validationResult = await validator.ValidateAsync(userRequestDto);

            if (!validationResult.IsValid)
            {
                // Validation hatalarını fırlat
                throw new ValidationException(validationResult.Errors);
            }
            // Email benzersizliği kontrolü
            var existingUser = await _userRepository.GetByEmailAsync(userRequestDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("This email is already registered.");
            }

            // Yeni kullanıcıyı oluştur
            var user = new User
            {
                FirstName = userRequestDto.FirstName,
                LastName = userRequestDto.LastName,
                Email = userRequestDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                Role = "User"
            };

            // Kullanıcıyı veritabanına ekle
            await _userRepository.AddAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = GenerateJwtToken(user)
            };
        }

        public async Task<UserResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            // E-posta ile kullanıcıyı bul
            var user = await _userRepository.FindByEmailAsync(loginRequestDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = GenerateJwtToken(user)
            };
        }

        public async Task<UserResponseDto> UpdatePasswordAsync(int userId, string newPassword)
        {
            // Kullanıcıyı veritabanından bul
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found.");

            // Şifreyi güncelle
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.Update(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = GenerateJwtToken(user)
            };
        }

        public async Task ActivateUserAsync(int userId)
        {
            // Kullanıcıyı aktif hale getir
            await _userRepository.ActivateUserAsync(userId);
        }

        public async Task DeactivateUserAsync(int userId)
        {
            // Kullanıcıyı devre dışı bırak
            await _userRepository.DeactivateUserAsync(userId);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<IEnumerable<UserDto>> SearchUsersAsync(string query)
        {
            var users = await _userRepository.SearchUsersAsync(query);
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Language = u.Language,
                ProfilePictureUrl = u.ProfilePictureUrl,
                LastLoginAt = u.LastLoginAt
            });
        }

        public async Task<IEnumerable<UserDto>> GetChattedUsersAsync(int userId)
        {
            var users = await _userRepository.GetChattedUsersAsync(userId);
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Language = u.Language,
                ProfilePictureUrl = u.ProfilePictureUrl,
                LastLoginAt = u.LastLoginAt
            });
        }
    }
}
