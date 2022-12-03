using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.CustomPlaceService;

public interface ICustomPlaceService
{
    CustomPlace CreateCustomPlace(LobbyAtCustomPlaceRequest request, int userId);
    void DeleteCustomPlace(int customPlaceId);
    CustomPlace GetCustomPlaceById(int customPlaceId);
    CustomPlace UpdateCustomPlace(LobbyAtCustomPlaceRequest request);
}