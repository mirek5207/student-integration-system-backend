using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;

namespace student_integration_system_backend.Services.PlaceOwnerService;

public interface IPlaceOwnerService
{
    AuthenticationResponse RegisterPlaceOwner(PlaceOwnerSignUpRequest request);
    PlaceOwner GetPlaceOwnerByUserId(int userId);
    PlaceOwner UpdatePlaceOwnerByUserId(UpdatePlaceOwnerRequest request, int userId);
    IEnumerable<PlaceOwner> GetAllPlaceOwners();
}