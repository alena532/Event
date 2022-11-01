using AutoMapper;
using Events.Contracts.Responses.Companies;
using Events.Contracts.Responses.Speakers;
using Events.Models;

namespace Events.Profiles;

public class SpeakersMapper:Profile
{
    public SpeakersMapper()
    {
        CreateMap<Speaker, GetSpeakerResponse>();

    }
}