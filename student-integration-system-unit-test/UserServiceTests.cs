using System;
using System.Data.Common;
using NUnit.Framework;
using student_integration_system_backend.Entities;

using student_integration_system_backend.Services.UserService;

namespace student_integration_system_unit_test;

public class UserServiceTests
{
    private readonly IUserService _userService;
    private readonly AppDbContext _dbContext;

    private User? _user;
    private const int UserId = 1;
    private const string Email = "example@gmail.com";
    private const string Password = "password";
    private const string Login = "login";
    
    private User? _user2;
    private const int User2Id = 2;
    private const string Email2 = "example2@gmail.com";
    private const string Password2 = "password2";
    private const string Login2 = "login2";

    public UserServiceTests()
    {
        _dbContext = DataBaseSetup.SetUpDataBase();
        _userService = new UserServiceImpl(_dbContext);
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
        _user2 = new User
        {
            Id = User2Id,
            Email = Email2,
            HashedPassword = Password2,
            Login = Login2
        };
        
        _dbContext.Users.Add(_user);
        _dbContext.Users.Add(_user2);
        _dbContext.SaveChanges();
    }

    [Test]
    public void GetUserById_ShouldReturnUser_WhenUserExist()
    {
        //Act
        var result = _userService.GetUserById(UserId);
        //Assert
        Assert.AreEqual(_user,result);
    }

    [Test]
    public void GetUserByLogin_ShouldReturnUser_WhenUserExist()
    {
        //Act
        var result = _userService.GetUserByLogin(Login);
        //Assert
        Assert.AreEqual(_user,result);
    }
    
    [Test]
    public void CheckIfEmailIsUnique_ShouldReturnTrue_WhenAnotherUserDoesNotHaveSuchAnEmail()
    {
        //arrange
        const string email = "uniqueEmail@gmail.com";
        //act
        var result = _userService.CheckIfEmailIsUnique(UserId, email);
        //Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void CheckIfEmailIsUnique_ShouldReturnFalse_WhenAnotherUserHasSuchAnEmail()
    {
        //act
        var result = _userService.CheckIfEmailIsUnique(UserId, Email2);
        //Assert
        Assert.IsFalse(result);
    }
    
    [Test]
    public void CheckIfLoginIsUnique_ShouldReturnTrue_WhenAnotherUserDoesNotHaveSuchAnLogin()
    {
        //arrange
        const string login = "uniqueLogin";
        //act
        var result = _userService.CheckIfLoginIsUnique(UserId, login);
        //Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void CheckIfLoginIsUnique_ShouldReturnFalse_WhenAnotherUserHasSuchAnLogin()
    {
        //act
        var result = _userService.CheckIfLoginIsUnique(UserId, Login2);
        //Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void CreateUser_ShouldReturnUser_WhenUserIsCreated()
    {
        //arrange
        const string login = "test";
        const string email = "test@gmail.com";
        const string password = "test";
        //act
        var result = _userService.CreateUser(login,email,password);
        //Assert
        Assert.IsInstanceOf<User>(result);
        Assert.AreEqual(login,result.Login);
        Assert.AreEqual(email,result.Email);
        Assert.True(BCrypt.Net.BCrypt.Verify(password,result.HashedPassword));
    }
    [Test]
    public void CreateUser_ShouldReturnDbException_WhenMethodParametersAreNull()
    {
        //arrange
        string? login = null!;
        string? email = null!;
        string? password = null!;
       
        //Assert
        Assert.Throws<ArgumentNullException>(() => _userService.CreateUser(login,email,password));
    }
}