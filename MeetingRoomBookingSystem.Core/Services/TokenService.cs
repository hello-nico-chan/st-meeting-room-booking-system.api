using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetingRoomBookingSystem.Core.IRepositories;
using MeetingRoomBookingSystem.Core.IServices;
using MeetingRoomBookingSystem.Persistence.Models;
using Microsoft.IdentityModel.Tokens;

namespace MeetingRoomBookingSystem.Core.Services;

public class TokenService : ITokenService
{
    private readonly IGeneralRepository<RefreshToken> _repository;
    private readonly IUserService _userService;

    public TokenService(IUserService userService, IGeneralRepository<RefreshToken> repository)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<string> GenerateAccessTokenAsync(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) return string.Empty;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Token.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwtToken = new JwtSecurityToken(
            Constants.Token.Issuer,
            Constants.Token.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(Constants.Token.AccessExpiration),
            signingCredentials: credentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken)!;

        return token;
    }

    public async Task<string> GenerateRefreshTokenAsync(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Constants.Token.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }),
            Expires = DateTime.UtcNow.AddMinutes(Constants.Token.AccessExpiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var refreshToken = tokenHandler.CreateToken(tokenDescriptor);
        var refreshTokenString = tokenHandler.WriteToken(refreshToken);

        var existedModels = await _repository.GetAsync(x => x.UserId == Guid.Parse(userId));
        var existedModel = existedModels.FirstOrDefault();
        if (existedModel == null)
        {
            await _repository.InsertAsync(new RefreshToken
            {
                UserId = Guid.Parse(userId),
                Token = refreshTokenString,
                CreatedAt = DateTime.UtcNow
            });
            await _repository.SaveAsync();
        }
        else
        {
            existedModel.Token = refreshTokenString;
            existedModel.LastRequestTime = DateTime.UtcNow;

            _repository.Update(existedModel);
            await _repository.SaveAsync();
        }
        return refreshTokenString;
    }

    public async Task<string> GetRefreshTokenAsync(string userId)
    {
        var refreshTokens = await _repository.GetAsync(x => x.UserId == Guid.Parse(userId));
        var refreshToken = refreshTokens.FirstOrDefault() ?? throw new Exception();
        return refreshToken.Token;
    }

    public async Task<string> UpdateRefreshTokenAsync(string userId)
    {
        var newRefreshToken = await GenerateAccessTokenAsync(userId);
        var existedRefreshTokenModels = await _repository.GetAsync(x => x.UserId == Guid.Parse(userId));
        var existedRefreshTokenModel = existedRefreshTokenModels.FirstOrDefault() ?? throw new Exception();

        existedRefreshTokenModel.Token = newRefreshToken;
        _repository.Update(existedRefreshTokenModel);
        await _repository.SaveAsync();

        return newRefreshToken;
    }

    public async Task UpdateLastRequestTimeAsync(string userId)
    {
        var existedRefreshTokenModels = await _repository.GetAsync(x => x.UserId == Guid.Parse(userId));
        var existedRefreshTokenModel = existedRefreshTokenModels.FirstOrDefault() ?? throw new Exception();

        existedRefreshTokenModel.LastRequestTime = DateTime.UtcNow;
        _repository.Update(existedRefreshTokenModel);
        await _repository.SaveAsync();
    }

    public async Task CheckRefreshTokenExpired(string userId)
    {
        var refreshTokenModels = await _repository.GetAsync(x => x.UserId == Guid.Parse(userId));
        var refreshTokenModel = refreshTokenModels.FirstOrDefault() ?? throw new Exception();

        if (refreshTokenModel.LastRequestTime.AddMonths(1) < DateTime.UtcNow)
        {
            throw new Exception();
        }
    }
}
