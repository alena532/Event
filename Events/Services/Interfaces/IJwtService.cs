using Events.Models;

namespace Events.Service.IService;

public interface IJwtService
{
    string GenerateJwtTokenAsync(User user);
}

