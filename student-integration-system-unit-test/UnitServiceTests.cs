using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Services.AccountService;
using student_integration_system_backend.Services.UserRoleService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_unit_test;

public class UserServiceTests
{
    private readonly IUserService _userService;
    private readonly AppDbContext _dbContext;
    private readonly Mock<IAccountService> _accountMock = new ();
    private readonly Mock<IUserRoleService> _userRoleMock = new ();

    private User? _user;
    private const int UserId = 1;
    private const string Email = "example@gmail.com";
    private const string Password = "password";
    private const string Login = "login";

    public UserServiceTests()
    {
        _dbContext = DataBaseSetup.SetUpDataBase();
        _userService = new UserServiceImpl(_dbContext, _accountMock.Object, _userRoleMock.Object);
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
        _dbContext.Users.Add(_user);
        _dbContext.SaveChanges();
    }

    [Test]
    public void GetUserById_ShouldReturnUser_WhenUserExist()
    {
        //Act
        var result = _userService?.GetUserById(UserId);
        //Assert
        Assert.AreEqual(_user,result);
    }
    
}