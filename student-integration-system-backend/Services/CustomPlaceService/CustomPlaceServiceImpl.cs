using Microsoft.EntityFrameworkCore;
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

    public CustomPlace CreateCustomPlace(LobbyAtCustomPlaceRequest request, int userId)
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
    
    public CustomPlace UpdateCustomPlace(LobbyAtCustomPlaceRequest request)
    {
        var customPlace = GetCustomPlaceById((int)request.CustomPlaceId!);
        customPlace.Name = request.CustomPlaceName;
        customPlace.Latitude = request.Latitude;
        customPlace.Longitude = request.Longitude;
        customPlace.Description = request.Description;
        _dbContext.SaveChanges();
        return customPlace;
    }

    public IEnumerable<CustomPlace> GetClientCustomPlaces(int userId)
    {
        var client = _clientService.GetClientByUserId(userId);
        var customPlaces = _dbContext.CustomPlaces.Where(cp => cp.ClientId == client.Id).ToList();
        if (customPlaces.Count == 0)
        {
            throw new NotFoundException("Custom places not found");
        }

        foreach (var cp in customPlaces)
        {
            cp.Client = null;
        }
        return customPlaces;
    }

    public CustomPlace GetCustomPlaceById(int customPlaceId)
    {
        var customPlace = _dbContext.CustomPlaces.FirstOrDefault(place => place.Id == customPlaceId);
        if (customPlace == null) throw new NotFoundException("Custom place not found");
        return customPlace;
    }
}