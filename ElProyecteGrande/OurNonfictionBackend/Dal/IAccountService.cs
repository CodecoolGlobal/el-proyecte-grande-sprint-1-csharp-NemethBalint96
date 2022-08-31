using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Dal;

public interface IAccountService
{
    Task<List<Account>> GetAll();
    Task<Account> Get(string username);
    Task Registration(Account account);
    Task<bool> CheckUserName(string username);
    Task<Account?> AuthenticateAsync(string username, string password);
    Task<bool> CheckEmail(string email);
    void SendPasswordChangeEmail(string email);
    Task ChangePasswordForUser(string username, string password);
}