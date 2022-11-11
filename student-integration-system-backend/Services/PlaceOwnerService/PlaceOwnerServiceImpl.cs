using System.Globalization;
using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
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
    
    public PlaceOwnerServiceImpl(AppDbContext dbContext, IUserService userService, IAuthService authService, IRoleService roleService)
    {
        _dbContext = dbContext;
        _userService = userService;
        _authService = authService;
        _roleService = roleService;
    }

    public AuthenticationResponse RegisterPlaceOwner(PlaceOwnerSignUpRequest request)
    {
        var role = _roleService.GetRoleById(RoleType.PlaceOwnerId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword, role);
        CreatePlaceOwner(user,request.FirstName, request.SurName);
        return _authService.GenerateJwtToken(user);
    }

    public PlaceOwner GetPlaceOwnerByUserId(int userId)
    {
        var placeOwner = _dbContext.PlacesOwners.FirstOrDefault(owner => owner.UserId == userId);
        if (placeOwner == null) throw new NotFoundException("Place owner not found");
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