using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurNonfictionBackend.Auth;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IJWTAuthenticationManager _JWTAuthenticationManager;

    public AccountController(IAccountService accountService, IJWTAuthenticationManager jwtAuthenticationManager)
    {
        _accountService = accountService;
        _JWTAuthenticationManager = jwtAuthenticationManager;
    }

    [HttpPost("registration")]
    public async Task<OkObjectResult> Registration(Account account)
    {
        await _accountService.Registration(account);
        return Ok("Ok");
    }

    [HttpPost("checkname")]
    public async Task<bool> CheckUserName([FromBody] string username)
    {
        return await _accountService.CheckUserName(username);
    }

    [HttpPost("checkemail")]
    public async Task<bool> CheckEmail([FromBody] string email)
    {
        return await _accountService.CheckEmail(email);
    }

    [HttpPost("passwordchange")]
    public void SendPasswordRecoveryEmail([FromBody] string email)
    {
        _accountService.SendPasswordChangeEmail(email);
    }

    [HttpPost("passwordchange/{username}")]
    public async Task ChangePasswordForUser([FromBody] string password, [FromRoute] string username)
    {
        await _accountService.ChangePasswordForUser(username, password);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Account account)
    {
        var token = await _JWTAuthenticationManager.Authenticate(account.Username, account.Password);

        if (token is null)
            return Unauthorized();

        var user = await _accountService.Get(account.Username);
        return Ok(new { Token = token, role = user.Role });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<List<Account>> GetAccounts()
    {
        return await _accountService.GetAll();
    }

    [HttpPost("signin-google")]
    public async Task<IActionResult> LoginWithGoogle(Account account)
    {
        await _accountService.Registration(account);
        var token = _JWTAuthenticationManager.WriteToken(account);

        return Ok(new { Token = token, role = account.Role });
    }

    [HttpGet("client-id")]
    public IActionResult GetClientId()
    {
        return Ok(new {
            Result= _JWTAuthenticationManager.GetClientId()
        });
    }
}