using Events.Contracts.Responses.Users;

namespace Events.Contracts.Responses.Auth;

public class LoginResponse
{
    public string Token { get; set; }
    public GetAdminResponse User { get; set; }
}