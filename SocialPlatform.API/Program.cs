using Microsoft.EntityFrameworkCore;
using SocialPlatform.Data;
using SocialPlatform.Core.Mappings;
using SocialPlatform.Core.Entities;
using SocialPlatform.Core.Mappings;
using SocialPlatform.Data.IRepo;
using SocialPlatform.Data.Repo;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using SocialPlatform.Service;
using SocialPlatform.Service.Service;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// JWT Anahtar�n� Base64 format�na d�n��t�r�n
var key = Encoding.UTF8.GetBytes("ThisIsA256BitSecretKeyForJWTToken123"); // 32 byte'l�k anahtar
var base64Key = Convert.ToBase64String(key); // Base64 format�na d�n��t�r�lm�� anahtar

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Jwt:Key"]))  // Base64 anahtar�n� ��zme
        };
    });



// Add services to the container.
builder.Services.AddControllers();


// Veritaban� ba�lant�s�n� ekliyoruz (Connection String'i appsettings.json'dan al�yoruz)
builder.Services.AddDbContext<SocialPlatformContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), // appsettings.json i�indeki ba�lant� dizesi
        b => b.MigrationsAssembly("SocialPlatform.Data") // Migration i�lemleri i�in proje belirtiyoruz
    ));

// Swagger/OpenAPI'yi yap�land�r�yoruz (API dok�mantasyonu i�in)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
// HTTPS y�nlendirme
app.UseHttpsRedirection();

// Authorization middleware
app.UseAuthorization();

// Controller'lar� haritalama (API yollar�n� yap�land�r�r)
app.MapControllers();

// Uygulamay� ba�lat
app.Run();
