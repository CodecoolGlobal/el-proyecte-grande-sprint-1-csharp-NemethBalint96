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
        return Ok("Kész");
    }

    [HttpPost("checkname")]
    public async Task<bool> CheckUserName([FromBody] string username)
    {
        return await _accountService.CheckUserName(username);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Account account)
    {
        var token = await _JWTAuthenticationManager.Authenticate(account.Username, account.Password);

        if (token is null)
            return Unauthorized();

        return Ok(token);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<List<Account>> GetAccounts()
    {
        return await _accountService.GetAll();
    }
}