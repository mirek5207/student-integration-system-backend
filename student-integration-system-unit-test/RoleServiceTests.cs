using NUnit.Framework;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Services.RoleService;

namespace student_integration_system_unit_test;

public class RoleServiceTests
{
    private readonly AppDbContext _dbContext;
    private readonly IRoleService _roleService;

    private Role? _role;
    private const int RoleId = 1;
    private const string RoleName = "Client";
    private const string ErrorMessage = "Role not found";
    
    public RoleServiceTests()
    {
        _dbContext = DataBaseSetup.SetUpDataBase();
        _roleService = new RoleServiceImpl(_dbContext);
    }

    [OneTimeSetUp]
    public void Setup()
    {
        _role = new Role
        {
            Id = RoleId,
            Name = RoleName
        };
        _dbContext.Add(_role);
        _dbContext.SaveChanges();
    }
    [Test]
    public void GetRoleById_ShouldReturnRole_WhenExist()
    {
        //Act
        var result = _roleService.GetRoleById(RoleId);
        //Assert
        Assert.AreEqual(_role,result);
    }
    [Test]
    public void GetRoleById_ShouldThrowNotFoundExceptionWithMessage_WhenRoleNotExist()
    {
        //Arrange
        const int fakeRoleId = 100;
        //Act
        var result = Assert.Throws<NotFoundException>(() =>_roleService.GetRoleById(fakeRoleId));
        //Assert
        Assert.AreEqual(ErrorMessage,result!.Message);
    }
    
}