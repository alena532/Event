using AutoMapper;
using Events.Contracts.Responses.Events;
using Events.Models;

namespace Events.Profiles;

public class EventsMapper:Profile
{
    public EventsMapper()
    {
        CreateMap<Event, GetEventResponse>()
            .ForMember("Speaker", opt => opt.MapFrom(x => x.Speaker.FirstName))
            .ForMember("Company", opt => opt.MapFrom(x => x.Company.Name));
    }
}