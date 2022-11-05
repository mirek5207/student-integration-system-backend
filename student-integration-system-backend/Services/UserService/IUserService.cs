using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.UserService;

public interface IUserService
{
    User CreateUser(string login, string email, string password, Role role);
    User GetUserByLogin(string login);
    User GetUserById(int userId);
    bool CheckIfEmailIsUnique(int userId, string email);
    bool CheckIfLoginIsUnique(int userId, string login);
}