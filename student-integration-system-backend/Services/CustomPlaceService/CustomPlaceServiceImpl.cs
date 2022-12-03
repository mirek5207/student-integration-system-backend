using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Services.ClientService;

namespace student_integration_system_backend.Services.CustomPlaceService;

public class CustomPlaceServiceImpl : ICustomPlaceService
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _clientService;

    public CustomPlaceServiceImpl(AppDbContext dbContext, IClientService clientService)
    {
        _dbContext = dbContext;
        _clientService = clientService;
    }

    public CustomPlace CreateCustomPlace(CreateLobbyAtCustomPlaceRequest request, int userId)
    {
        var place = new CustomPlace
        {
            Name = request.CustomPlaceName,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Client = _clientService.GetClientByUserId(userId),
            Description = request.Description
        };
        _dbContext.CustomPlaces.Add(place);
        _dbContext.SaveChanges();
        return place;
    }

    public void DeleteCustomPlace(int customPlaceId)
    {
        var customPlace = GetCustomPlaceById(customPlaceId);
        _dbContext.CustomPlaces.Remove(customPlace);
        _dbContext.SaveChanges();
    }
    
    public CustomPlace UpdateCustomPlace(int customPlaceId, UpdateCustomPlaceRequest request)
    {
        var customPlace = GetCustomPlaceById(customPlaceId);
        customPlace.Latitude = request.Latitude;
        customPlace.Longitude = request.Longitude;
        customPlace.Description = request.Description;
        _dbContext.SaveChanges();
        return customPlace;
    }
    
    public CustomPlace GetCustomPlaceById(int customPlaceId)
    {
        var customPlace = _dbContext.CustomPlaces.FirstOrDefault(place => place.Id == customPlaceId);
        if (customPlace == null) throw new NotFoundException("Custom place not found");
        return customPlace;
    }
}