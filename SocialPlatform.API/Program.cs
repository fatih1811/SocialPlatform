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

// JWT Anahtarýný Base64 formatýna dönüþtürün
var key = Encoding.UTF8.GetBytes("ThisIsA256BitSecretKeyForJWTToken123"); // 32 byte'lýk anahtar
var base64Key = Convert.ToBase64String(key); // Base64 formatýna dönüþtürülmüþ anahtar

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
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Jwt:Key"]))  // Base64 anahtarýný çözme
        };
    });



// Add services to the container.
builder.Services.AddControllers();


// Veritabaný baðlantýsýný ekliyoruz (Connection String'i appsettings.json'dan alýyoruz)
builder.Services.AddDbContext<SocialPlatformContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), // appsettings.json içindeki baðlantý dizesi
        b => b.MigrationsAssembly("SocialPlatform.Data") // Migration iþlemleri için proje belirtiyoruz
    ));

// Swagger/OpenAPI'yi yapýlandýrýyoruz (API dokümantasyonu için)
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
// HTTPS yönlendirme
app.UseHttpsRedirection();

// Authorization middleware
app.UseAuthorization();

// Controller'larý haritalama (API yollarýný yapýlandýrýr)
app.MapControllers();

// Uygulamayý baþlat
app.Run();
