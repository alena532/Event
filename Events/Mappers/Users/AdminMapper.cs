using AutoMapper;
using Events.Contracts.Responses.Users;
using Events.Models;

namespace Events.Profiles;

public class AdminMapper:Profile
{
    public AdminMapper()
    {
        CreateMap<User, GetAdminResponse>()
            .ForMember("Role", opt => opt.MapFrom(x => x.Role.Name));
    }
}
