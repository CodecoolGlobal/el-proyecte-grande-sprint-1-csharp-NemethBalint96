using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Helpers;
using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Dal;

public class AccountService : IAccountService
{
    private readonly NonfictionContext _context;

    public AccountService(NonfictionContext context)
    {
        _context = context;
    }

    public Task<List<Account>> GetAll()
    {
        return _context.Accounts.ToListAsync();
    }

    public async Task<Account?> Get(string username)
    {
        return await _context.Accounts.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task Registration(Account account)
    {
        account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        EmailHelper.CreateWelcomeMessage(account.Username, account.Email);
    }

    public async Task<bool> CheckUserName(string username)
    {
        return await _context.Accounts.AnyAsync(user => user.Username == username);
    }

    public async Task<Account?> AuthenticateAsync(string username, string password)
    {
        _context.ChangeTracker.Clear();
        var user = await _context.Accounts.SingleOrDefaultAsync(user => user.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            return null;

        return user;
    }

    public async Task<bool> CheckEmail(string email)
    {
        return await _context.Accounts.AnyAsync(user => user.Email == email);
    }

    public void SendPasswordChangeEmail(string email)
    {
        var username = _context.Accounts.FirstOrDefault(user => user.Email == email).Username;
        var encodedUsername = EncodeDecodeHelper.encode(username);
        var link = $"https://our-nonfiction.herokuapp.com/forgot/{encodedUsername}";
        EmailHelper.CreatePasswordRecoveryEmail(email, link, username);
    }

    public async Task ChangePasswordForUser(string username, string password)
    {
        _context.ChangeTracker.Clear();
        var decodedUsername = EncodeDecodeHelper.decode(username);
        var user = await _context.Accounts.FirstAsync(user => user.Username == decodedUsername);
        user.Password = BCrypt.Net.BCrypt.HashPassword(password);
        _context.Accounts.Update(user);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
    }
}