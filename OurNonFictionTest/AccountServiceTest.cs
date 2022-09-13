using Microsoft.EntityFrameworkCore;
using NSubstitute;
using OurNonfictionBackend.Dal;
using OurNonfictionBackend.Helpers;
using OurNonfictionBackend.Models;

namespace OurNonFictionTest;

internal class AccountServiceTest
{
    private IAccountService _accountService;
    private NonfictionContext _context;

    [SetUp]
    public void Setup()
    {
        _accountService = Substitute.For<AccountService>(Substitute.For<InitDatabase>().CreateContext());
        _context = Substitute.For<InitDatabase>().CreateContext();
    }

    [Test]
    public void GetAll_ReturnsAllAccounts()
    {
        var expected = Task.Run(() => _context.Accounts.ToListAsync()).Result.Count;
        var actual = Task.Run(() => _accountService.GetAll()).Result.Count;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Get_ExistingUsernameParam_ReturnAccount()
    {
        var expected = Task.Run(() => _context.Accounts.FirstAsync()).Result.Username;
        var actual = Task.Run(() => _accountService.Get(expected)).Result?.Username;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public async Task Registration_AddsAnAccount()
    {
        var newAccount = new Account
        {
            Email = "test@test.test",
            Username = "test",
            Password = BCrypt.Net.BCrypt.HashPassword("test"),
            Role = "User"
        };
        var expected = Task.Run(() => _context.Accounts.ToListAsync()).Result.Count + 1;
        await _accountService.Registration(newAccount);
        var actual = Task.Run(() => _context.Accounts.ToListAsync()).Result.Count;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public async Task CheckUsername_CheckExistingUsername_ReturnTrue()
    {
        var username = Task.Run(() => _context.Accounts.FirstAsync()).Result.Username;
        var actual = await _accountService.CheckUserName(username);
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task AuthenticateAsync_AuthenticateExistingAccount_ReturnsExistingAccount()
    {
        const string username = "admin";
        const string password = "admin";
        var account = await _accountService.AuthenticateAsync(username, password);
        Assert.That(account.Username, Is.EqualTo(username));
    }

    [Test]
    public async Task CheckEmail_CheckExistingEmail_ReturnsTrue()
    {
        var email = Task.Run(() => _context.Accounts.FirstAsync()).Result.Email;
        var actual = await _accountService.CheckEmail(email);
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task ChangePasswordForUser_ExistingUser_PasswordChanged()
    {
        var account = Task.Run(() => _context.Accounts.FirstAsync()).Result;
        var username = account.Username;
        var password = account.Password;
        var encodedUsername = EncodeDecodeHelper.encode(username);
        const string newPassword = "new password";
        await _accountService.ChangePasswordForUser(encodedUsername, newPassword);
        var changedPassword = Task.Run(() => _accountService.Get(username)).Result?.Password;
        Assert.That(password, Is.Not.EqualTo(changedPassword));
    }
}
