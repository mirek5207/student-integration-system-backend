using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.PlaceOwnerService;

public interface IPlaceOwnerService
{
    PlaceOwner RegisterPlaceOwner(PlaceOwnerSignUpRequest request);
}