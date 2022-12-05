using System.Linq;
using NUnit.Framework;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Services.UserRoleService;

namespace student_integration_system_unit_test;

public class UserRoleServiceTests
{
    private readonly AppDbContext _dbContext;
    private readonly IUserRoleService _userRoleService;

    private Role? _role;
    private const int RoleId = 1;
    private const string RoleName = "Client";
    
    private User? _user;
    private const int UserId = 1;
    private const string Email = "example@gmail.com";
    private const string Password = "password";
    private const string Login = "login";

    private const string ExceptionMessage = "Role not found";

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
            Id = RoleId,
            Name = RoleName
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
            Login = "login2",
            HashedPassword = "password"
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        var role = _role;
        //Act
        _userRoleService.CreateUserRole(user, role!);
        var result = _dbContext.UserRoles.FirstOrDefault(userRole => userRole.RoleId == _role!.Id && userRole.UserId == user.Id);
        //Assert
        Assert.IsInstanceOf<UserRole>(result);
        Assert.AreEqual(user,result!.User);
        Assert.AreEqual(role,result!.Role);
    }

    [Test]
    public void GetUserRole_ShouldReturnRole_WhenExist()
    {
        //Act
        var result = _userRoleService.GetUserRole(_user!);
        //Assert
        Assert.AreEqual(_role!,result);
    }

    [Test]
    public void GetUserRole_ShouldThrowNotFoundExceptionWithMessage_WhenNotExist()
    {
        //Arrange
        var user = new User();
        //Act
        var result = Assert.Throws<NotFoundException>(() => _userRoleService.GetUserRole(user));
        //Assert
        Assert.AreEqual(ExceptionMessage,result!.Message);
    }
    
}