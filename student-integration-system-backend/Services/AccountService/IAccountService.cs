using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.AccountService;

public interface IAccountService
{
    void CreateAccount(User user);
    Account GetAccountByUserId(int userId);
    Account UpdateStatusOfUserAccount(int userId,bool isActive);
}