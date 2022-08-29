using Microsoft.AspNetCore.Mvc;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Controllers;
[ApiController, Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("registration")]
    public async Task Registration(Account account)
    {
        await _accountService.Registration(account);
    }
}