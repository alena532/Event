using AutoMapper;
using Events.DTO;
using Events.Models;

namespace Events.Profiles;

public class EventCreatingProfile:Profile
{
    public EventCreatingProfile()
    {
        CreateMap<EventCreateDTO, Event>();
    }
    
}