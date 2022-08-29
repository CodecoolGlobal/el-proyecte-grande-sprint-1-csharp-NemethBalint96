﻿using Microsoft.EntityFrameworkCore;
using OurNonfictionBackend.Helpers;
using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Dal;

class AccountService : IAccountService
{
    private readonly NonfictionContext _context;

    public AccountService(NonfictionContext context)
    {
        _context = context;
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
}