using AutoMapper;
using Events.Contracts.Responses.Events;
using Events.Contracts.Responses.Users;
using Events.Models;

namespace Events.Profiles;

public class HospitalWorkerMapper:Profile
{
    public HospitalWorkerMapper()
    {
        CreateMap<User, GetCompanyWorkerResponse>()
            .ForMember("Role", opt => opt.MapFrom(x => x.Role.Name))
            .ForMember("Company", opt => opt.MapFrom(x => x.Company.Name));
    }
}