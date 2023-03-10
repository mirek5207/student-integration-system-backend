using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;

namespace student_integration_system_backend.Services.ClientService;

public interface IClientService
{
    AuthenticationResponse RegisterClient(ClientSignUpRequest request);
    Client UpdateClient(UpdateClientRequest request, int userId);
    Client GetClientById(int userId);
    IEnumerable<Client> GetAllClients();
    IEnumerable<Client> GetAllClientsExceptActiveUser(int userId);
    Client GetClientByUserId(int userId);
    IEnumerable<Client> GetAllClientExceptFriends(int userId);
    IEnumerable<Client> GetClientsFromFriendshipsExceptActiveUser(IEnumerable<Friend> friends, int userId);
}