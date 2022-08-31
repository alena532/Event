using Events.Contracts.Requests.Users;
using Events.Contracts.Responses.Users;

namespace Events.Service.IService;

public interface IUsersService
{
    Task<GetAdminResponse> CreateAdminAsync(CreateAdminRequest request);
}