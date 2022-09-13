using ElProyecteGrande.Dal;
using NSubstitute;
using OurNonfictionBackend.Controllers;
using OurNonfictionBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OurNonfictionBackend.Auth;
using OurNonfictionBackend.Dal;
using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Helpers;
using OkObjectResult = Microsoft.AspNetCore.Mvc.OkObjectResult;

namespace OurNonFictionTest;

public class AccountControllerTest
{
    private NonfictionContext _context;
    private AccountService _accountService;
    private JWTAuthenticationManager _jwtAuthenticationManager;
    private AccountController _controller;

    [SetUp]
    public void Setup()
    {
        _context = Substitute.For<InitDatabase>().CreateContext();
        _accountService = Substitute.For<AccountService>(_context);
        _jwtAuthenticationManager = Substitute.For<JWTAuthenticationManager>("This is my test key", _accountService, "test");
        _controller = Substitute.For<AccountController>(_accountService, _jwtAuthenticationManager);
    }

    [Test]
    public void AccountController_GetAccount_ReturnsAllAccount()
    {
        var expected = _accountService.GetAll().Result.Count;
        var actual = _controller.GetAccounts().Result.Count;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Registration_AddsAnAccount()
    {
        var newAccount = new Account
        {
            Email = "test@test.test",
            Username = "test",
            Password = BCrypt.Net.BCrypt.HashPassword("test"),
            Role = "User"
        };
        var expected = _context.Accounts.ToListAsync().Result.Count + 1;
        var _ = _controller.Registration(newAccount).Result;
        var actual = _context.Accounts.ToListAsync().Result.Count;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CheckUsername_CheckExistingUsername_ReturnTrue()
    {
        var username = _context.Accounts.FirstAsync().Result.Username;
        var actual = _accountService.CheckUserName(username).Result;
        Assert.That(actual, Is.True);
    }

    [Test]
    public void CheckEmail_CheckExistingEmail_ReturnsTrue()
    {
        var email = _context.Accounts.FirstAsync().Result.Email;
        var actual = _accountService.CheckEmail(email).Result;
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task ChangePasswordForUser_ExistingUser_PasswordChanged()
    {
        var account = _context.Accounts.FirstAsync().Result;
        var username = account.Username;
        var password = account.Password;
        var encodedUsername = EncodeDecodeHelper.encode(username);
        const string newPassword = "new password";
        await _controller.ChangePasswordForUser(newPassword, encodedUsername);
        var changedPassword = _accountService.Get(username).Result?.Password;
        Assert.That(password, Is.Not.EqualTo(changedPassword));
    }
}
