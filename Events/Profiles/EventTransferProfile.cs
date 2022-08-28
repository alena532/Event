using AutoMapper;
using Events.DTO;
using Events.Models;

namespace Events.Profiles;

public class EventTransferProfile:Profile
{
    public EventTransferProfile()
    {
        CreateMap<Event, EventTransferDTO>();
    }
}