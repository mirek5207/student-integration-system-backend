using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;

namespace student_integration_system_backend.Services.ClientService;

public interface IClientService
{
    AuthenticationResponse RegisterClient(ClientSignUpRequest request);
    Client UpdateClient(UpdateClientRequest request, int clientId);
    Client GetClientById(int clientId);
    IEnumerable<Client> GetAllClients();
}