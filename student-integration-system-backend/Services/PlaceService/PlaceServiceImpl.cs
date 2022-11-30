using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
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

    public void DeletePlace(int placeId)
    {
        var place = GetPlaceById(placeId);
        _dbContext.Places.Remove(place);
        _dbContext.SaveChanges();
    }

    public Place UpdatePlace(int placeId, UpdatePlaceRequest request)
    {
        var place = GetPlaceById(placeId);
        place.Latitude = request.Latitude;
        place.Longitude = request.Longitude;
        place.Name = request.Name;
        place.NumberOfReservedSeats = request.NumberOfReservedSeats;
        place.MaxSeatsAvailableForReservation = request.MaxSeatsAvailableForReservation;
        _dbContext.SaveChanges();
        return place;
    }

    public Place GetPlaceById(int placeId)
    {
        var place = _dbContext.Places.FirstOrDefault(place => place.Id == placeId);
        if (place == null) throw new NotFoundException("Place not found");
        return place;
    }

    public IEnumerable<Place> GetAllPlacesOwnedByPlaceOwner(int userId)
    {
        var placeOwner = _ownerService.GetPlaceOwnerByUserId(userId);
        var places = _dbContext.Places.Where(p => p.PlaceOwner == placeOwner).ToList();
        return places;
    }
}