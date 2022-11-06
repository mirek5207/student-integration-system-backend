using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.PlaceService;

public interface IPlaceService
{
    Place CreatePlace(CreatePlaceRequest request);
    void DeletePlace(int placeId);
}