using student_integration_system_backend.Entities;
using student_integration_system_backend.Services.AccountService;
using student_integration_system_backend.Services.UserRoleService;

namespace student_integration_system_backend.Services.UserService;

public class UserServiceImpl : IUserService
{
    private readonly AppDbContext _dbContext;
    private readonly IUserRoleService _userRoleService;
    private readonly IAccountService _accountService;
    public UserServiceImpl(AppDbContext dbContext, IAccountService accountService, IUserRoleService userRoleService)
    {
        _dbContext = dbContext;
        _accountService = accountService;
        _userRoleService = userRoleService;
    }

    public User CreateUser(string login,string email,string password, Role role)
    {
        var user = new User
        {
            Login = login,
            Email = email,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(password)
        };
        _dbContext.Users.Add(user); 
        _dbContext.SaveChanges();
        
        _userRoleService.CreateUserRole(user, role);
        _accountService.CreateAccount(user);
        return user;

    }
}