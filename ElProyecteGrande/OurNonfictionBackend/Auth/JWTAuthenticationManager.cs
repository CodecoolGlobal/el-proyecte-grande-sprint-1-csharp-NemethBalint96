using Microsoft.IdentityModel.Tokens;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OurNonfictionBackend.Auth;

public interface IJWTAuthenticationManager
{
    Task<string?> Authenticate(string username, string password);
    string WriteToken(Account account);
    string GetClientId();
}

public class JWTAuthenticationManager : IJWTAuthenticationManager
{
    private readonly IAccountService _accountService;
    private readonly string _tokenKey;
    private readonly string _clientId;

    public JWTAuthenticationManager(string tokenKey, IAccountService accountService, string clientId)
    {
        _tokenKey = tokenKey;
        _accountService = accountService;
        _clientId = clientId;
    }

    public async Task<string?> Authenticate(string username, string password)
    {
        var user = await _accountService.AuthenticateAsync(username, password);
        if (user is null)
        {
            return null;
        }

        return WriteToken(user);
    }

    public string WriteToken(Account user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
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

    public string GetClientId()
    {
        return _clientId;
    }
}
