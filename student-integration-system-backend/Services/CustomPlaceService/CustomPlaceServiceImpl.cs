using student_integration_system_backend.Entities;
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

    public CustomPlace CreateCustomPlace(CreateCustomPlaceRequest request)
    {
        var place = new CustomPlace
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Client = _clientService.GetClientByUserId(request.UserId),
            Description = request.Description
        };
        _dbContext.CustomPlaces.Add(place);
        _dbContext.SaveChanges();
        return place;
    }
}