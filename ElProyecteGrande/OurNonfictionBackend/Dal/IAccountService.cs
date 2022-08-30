using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Dal;

public interface IAccountService
{
    Task<List<Account>> GetAll();
    Task<Account> Get(string username);
    Task Registration(Account account);
    Task<bool> CheckUserName(string username);
    Task<Account?> AuthenticateAsync(string username, string password);
}