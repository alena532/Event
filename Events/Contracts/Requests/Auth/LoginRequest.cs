namespace Events.Contracts.Requests.Auth;

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}