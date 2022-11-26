using System.Globalization;
using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.AccountService;
using student_integration_system_backend.Services.AuthService;
using student_integration_system_backend.Services.RoleService;
using student_integration_system_backend.Services.UserRoleService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Services.PlaceOwnerService;

public class PlaceOwnerServiceImpl : IPlaceOwnerService
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IRoleService _roleService;
    private readonly IAccountService _accountService;
    private readonly IUserRoleService _userRoleService;
    
    public PlaceOwnerServiceImpl(AppDbContext dbContext, IUserService userService, IAuthService authService,
        IRoleService roleService, IAccountService accountService, IUserRoleService userRoleService)
    {
        _dbContext = dbContext;
        _userService = userService;
        _authService = authService;
        _roleService = roleService;
        _accountService = accountService;
        _userRoleService = userRoleService;
    }

    public AuthenticationResponse RegisterPlaceOwner(PlaceOwnerSignUpRequest request)
    {
        var role = _roleService.GetRoleById(RoleType.PlaceOwnerId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword);
        _userRoleService.CreateUserRole(user, role);
        _accountService.CreateAccount(user);
        CreatePlaceOwner(user,request.FirstName, request.SurName);
        return _authService.GenerateJwtToken(user);
    }

    public PlaceOwner GetPlaceOwnerByUserId(int userId)
    {
        var placeOwner = _dbContext.PlacesOwners.Include(owner => owner.User.Account)
            .FirstOrDefault(owner => owner.UserId == userId);
        if (placeOwner == null) throw new NotFoundException("Place owner not found");
        return placeOwner;
    }

    public PlaceOwner UpdatePlaceOwnerByUserId(UpdatePlaceOwnerRequest request, int userId)
    {
        var placeOwner = GetPlaceOwnerByUserId(userId);

        placeOwner.FirstName = request.FirstName;
        placeOwner.SurName = request.SurName;
        placeOwner.User.Email = _userService.CheckIfEmailIsUnique(placeOwner.User.Id,request.Email) ? request.Email : throw new BadRequestException("Email already exist");
        placeOwner.User.Login = _userService.CheckIfLoginIsUnique(placeOwner.User.Id,request.Login) ? request.Login : throw new BadRequestException("Login already exist");
        placeOwner.User.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.HashedPassword);
        _accountService.UpdateStatusOfUserAccount(userId, request.IsActive);
        _dbContext.SaveChanges();
        
        return placeOwner;
    }

    public IEnumerable<PlaceOwner> GetAllPlaceOwners()
    {
        var placeOwners = _dbContext.PlacesOwners.Include(owner => owner.User.Account).ToList();
        return placeOwners;
    }

    private PlaceOwner CreatePlaceOwner(User user, string firstName, string surName)
    {
        var placeOwner = new PlaceOwner
        {
            User = user,
            FirstName = firstName,
            SurName = surName
        };
        _dbContext.PlacesOwners.Add(placeOwner);
        _dbContext.SaveChanges();
        return placeOwner;
    }
}