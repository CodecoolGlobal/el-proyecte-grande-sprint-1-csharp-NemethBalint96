using OurNonfictionBackend.Models;

namespace OurNonfictionBackend.Dal;

public interface IAccountService
{
    Task<Account> Get(string email);
    Task Registration(Account account);
    Task<bool> CheckUserName(string username);
}