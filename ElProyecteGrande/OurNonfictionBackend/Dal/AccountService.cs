using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Helpers;
using OurNonfictionBackend.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;

namespace OurNonfictionBackend.Dal;

class AccountService : IAccountService
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

    public Task<Account> Get(string email)
    {
        throw new NotImplementedException();
    }

    public async Task Registration(Account account)
    {
        account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        EmailHelper.SendEmail(account.Username,account.Email);
    }

    public async Task<bool> CheckUserName(string username)
    {
        return await _context.Accounts.AnyAsync(user => user.Username == username);
    }

    public async Task<Account?> AuthenticateAsync(string username, string password)
    {
        var user = await _context.Accounts.SingleOrDefaultAsync(user => user.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            return null;

        return user;
    }
}