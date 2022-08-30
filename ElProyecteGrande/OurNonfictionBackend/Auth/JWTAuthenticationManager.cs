using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Auth;

public interface IJWTAuthenticationManager
{
    Task<string?> Authenticate(string username, string password);
}

public class JWTAuthenticationManager : IJWTAuthenticationManager
{
    private readonly IAccountService _accountService;
    private readonly string _tokenKey;

    public JWTAuthenticationManager(string tokenKey, IAccountService accountService)
    {
        _tokenKey = tokenKey;
        _accountService = accountService;
    }

    public async Task<string?> Authenticate(string username, string password)
    {
        var user = await _accountService.AuthenticateAsync(username, password);
        if (user is null)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
