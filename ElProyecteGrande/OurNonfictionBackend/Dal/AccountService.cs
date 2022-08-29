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
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }
}