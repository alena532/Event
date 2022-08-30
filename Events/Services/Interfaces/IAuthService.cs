using Events.Contracts.Requests.Auth;
using Events.Contracts.Responses.Auth;

namespace Events.Service.IService;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}