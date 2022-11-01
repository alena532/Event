using AutoMapper;
using Events.Contracts.Responses.Companies;
using Events.Contracts.Responses.Events;
using Events.Models;

namespace Events.Profiles;

public class CompaniesMapper:Profile
{
    public CompaniesMapper()
    {
        CreateMap<Company, GetCompanyResponse>();

    }
}