using System.Text;
using MeetingRoomBookingSystem.Core.Constants;
using MeetingRoomBookingSystem.Core.IRepositories;
using MeetingRoomBookingSystem.Core.IServices;
using MeetingRoomBookingSystem.Core.Repositories;
using MeetingRoomBookingSystem.Core.Services;
using MeetingRoomBookingSystem.Persistence;
using MeetingRoomBookingSystem.Persistence.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Token.Secret)),
        ValidateIssuer = true,
        ValidIssuer = Token.Issuer,
        ValidateAudience = true,
        ValidAudience = Token.Audience,
        ValidateLifetime = true
    };
    x.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.Headers.Append("token-expired", "true");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddDbContext<MrbsDbContext>(optionsAction =>
    optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("MrbsConnectionString")));

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IMeetingRoomService, MeetingRoomService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IGeneralRepository<Booking>, GeneralRepository<Booking>>();
builder.Services.AddScoped<IGeneralRepository<RefreshToken>, GeneralRepository<RefreshToken>>();
builder.Services.AddScoped<IGeneralRepository<MeetingRoom>, GeneralRepository<MeetingRoom>>();
builder.Services.AddScoped<IGeneralRepository<User>, GeneralRepository<User>>();

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseCors(options => options
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
