using System.Globalization;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.AuthService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Services.PlaceOwnerService;

public class PlaceOwnerServiceImpl : IPlaceOwnerService
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    public PlaceOwnerServiceImpl(AppDbContext dbContext, IUserService userService, IAuthService authService)
    {
        _dbContext = dbContext;
        _userService = userService;
        _authService = authService;
    }

    public AuthenticationResponse RegisterPlaceOwner(PlaceOwnerSignUpRequest request)
    {
        var role = _dbContext.Roles.Find(RoleType.PlaceOwnerId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword, role);
        var placeOwner = CreatePlaceOwner(user,request.FirstName, request.SurName);
        return _authService.GenerateJwtToken(user);
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