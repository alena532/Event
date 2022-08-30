using AutoMapper;
using Events.Contracts.Requests.Events;
using Events.Models;

namespace Events.Profiles;

public class EventCreatingProfile:Profile
{
    public EventCreatingProfile()
    {
        CreateMap<CreateEventRequest, Event>();
    }
    
}