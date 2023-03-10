using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Services.AccountService;
using student_integration_system_backend.Services.UserRoleService;

namespace student_integration_system_backend.Services.UserService;

public class UserServiceImpl : IUserService
{
    private readonly AppDbContext _dbContext;
    
    public UserServiceImpl(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User CreateUser(string login,string email,string password)
    {
        var user = new User
        {
            Login = login,
            Email = email,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(password)
        };
        _dbContext.Users.Add(user); 
        _dbContext.SaveChanges();
        
        return user;

    }
    
    public User GetUserById(int userId)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Id == userId);
        if (user == null) throw new NotFoundException("User not found");
        return user;
    }
    public User GetUserByLogin(string login)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Login == login);
        if (user == null) throw new NotFoundException("User not found");
        return user;
    }

    public bool CheckIfEmailIsUnique(int userId, string email)
    {
        var emailExist = _dbContext.Users.Any(user => user.Email == email && user.Id != userId);
        return !emailExist;
    }
    
    public bool CheckIfLoginIsUnique(int userId, string login)
    {
        var loginExist = _dbContext.Users.Any(user => user.Login == login && user.Id != userId);
        return !loginExist;
    }
}