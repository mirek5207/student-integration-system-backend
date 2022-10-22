using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;

namespace student_integration_system_backend.Services.ModeratorService;

public interface IModeratorService
{
   AuthenticationResponse RegisterModerator(ModeratorSignUpRequest request);
}