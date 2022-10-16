using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.ClientService;

public interface IClientService
{
    Client RegisterClient(ClientSignUpRequest request);
}