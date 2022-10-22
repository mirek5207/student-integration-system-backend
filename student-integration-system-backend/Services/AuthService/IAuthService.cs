using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;

namespace student_integration_system_backend.Services.AuthService;

public interface IAuthService
{
    AuthenticationResponse GenerateJwtToken(User user);
    AuthenticationResponse AuthUser(SignInRequest request);
}