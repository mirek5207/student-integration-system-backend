using System.Linq;
using NUnit.Framework;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Services.UserRoleService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_unit_test;

public class UserRoleServiceTests
{
    private readonly AppDbContext _dbContext;
    private readonly IUserRoleService _userRoleService;

    private Role? _role;
    private User? _user;
    private const int UserId = 1;
    private const string Email = "example@gmail.com";
    private const string Password = "password";
    private const string Login = "login";

    public UserRoleServiceTests()
    {
        _dbContext = DataBaseSetup.SetUpDataBase();
        _userRoleService = new UserRoleServiceImpl(_dbContext);
    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        _user = new User
        {
            Id = UserId,
            Email  = Email,
            HashedPassword = Password,
            Login = Login
        };
        _role = new Role
        {
            Id = 1,
            Name = "Client"
        };
        _dbContext.Users.Add(_user);
        _dbContext.Roles.Add(_role);
        _dbContext.UserRoles.Add(new UserRole {Role = _role, User = _user});
        _dbContext.SaveChanges();
    }

    [Test]
    public void CreateUserRole_ShouldBeAddedToDatabase_WhenItIsCreated()
    {
        //Arrange
        var user = new User
        {
            Id = 2,
            Email = "test@gmail.com",
            Login = "login1",
            HashedPassword = "password"
        };
        var role = _role;
        //Act
        _userRoleService.CreateUserRole(user, role!);
        var result = _dbContext.UserRoles.FirstOrDefault(userRole => userRole.RoleId == _role!.Id && userRole.UserId == _user!.Id);
        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<UserRole>(result);
    }
}