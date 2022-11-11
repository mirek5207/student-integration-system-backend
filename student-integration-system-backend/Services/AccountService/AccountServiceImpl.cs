using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;

namespace student_integration_system_backend.Services.AccountService;

public class AccountServiceImpl : IAccountService
{
    private readonly AppDbContext _dbContext;


    public AccountServiceImpl(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateAccount(User user)
    {
        var account = new Account
        {
            User = user,
            AccountCreationTime = DateTime.Now,
            IsActive = true
        };
        _dbContext.Accounts.Add(account); 
        _dbContext.SaveChanges();
    }

    public Account GetAccountByUserId(int userId)
    {
        var account = _dbContext.Accounts.FirstOrDefault(account => account.UserId == userId);
        if (account == null) throw new NotFoundException("Account not found");
        return account;
    }

    public Account UpdateStatusOfUserAccount(int userId, bool isActive)
    {
        var account = GetAccountByUserId(userId);
        account.IsActive = isActive;
        _dbContext.SaveChanges();
        return account;
    }
}