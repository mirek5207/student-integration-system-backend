using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.PlaceService;

public interface IPlaceService
{
    Place CreatePlace(CreatePlaceRequest request);
    void DeletePlace(int placeId);
    Place UpdatePlace(int placeId, UpdatePlaceRequest request);

    Place GetPlaceById(int placeId);
    IEnumerable<Place> GetAllPlacesOwnedByPlaceOwner(int userId);
    IEnumerable<Place> GetAllPlaces();
}