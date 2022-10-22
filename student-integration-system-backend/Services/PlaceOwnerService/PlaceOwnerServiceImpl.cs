using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Services.PlaceOwnerService;

public class PlaceOwnerServiceImpl : IPlaceOwnerService
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _userService;

    public PlaceOwnerServiceImpl(AppDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public PlaceOwner RegisterPlaceOwner(PlaceOwnerSignUpRequest request)
    {
        var role = _dbContext.Roles.Find(RoleType.PlaceOwnerId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword, role);
        var placeOwner = CreatePlaceOwner(user,request.FirstName, request.SurName);
        return placeOwner;
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