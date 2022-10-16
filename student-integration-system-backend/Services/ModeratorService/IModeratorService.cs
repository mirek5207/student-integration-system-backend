using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.ModeratorService;

public interface IModeratorService
{
   Moderator RegisterModerator(ModeratorSignUpRequest request);
}