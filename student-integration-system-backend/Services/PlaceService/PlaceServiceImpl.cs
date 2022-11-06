using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Services.PlaceOwnerService;

namespace student_integration_system_backend.Services.PlaceService;

public class PlaceServiceImpl : IPlaceService
{
    private readonly IPlaceOwnerService _ownerService;
    private readonly AppDbContext _dbContext;

    public PlaceServiceImpl(IPlaceOwnerService ownerService, AppDbContext dbContext)
    {
        _ownerService = ownerService;
        _dbContext = dbContext;
    }

    public Place CreatePlace(CreatePlaceRequest request)
    {
        var place = new Place
        {
            Name = request.Name,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            PlaceOwner = _ownerService.GetPlaceOwnerByUserId(request.UserId),
            NumberOfReservedSeats = 0,
            MaxSeatsAvailableForReservation = request.MaxSeatsAvailableForReservation,
        };
        _dbContext.Places.Add(place);
        _dbContext.SaveChanges();
        return place;
    }
}