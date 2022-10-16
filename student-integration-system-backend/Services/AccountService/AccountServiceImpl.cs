using student_integration_system_backend.Entities;

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
}