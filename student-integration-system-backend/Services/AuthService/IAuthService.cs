using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Response;

namespace student_integration_system_backend.Services.AuthService;

public interface IAuthService
{
    // AuthenticationResponse? Authenticate(AuthenticationRequest request);
    AuthenticationResponse GenerateJwtToken(User user);
}