using Events.Contracts.Responses.Speakers;
using Events.Models;

namespace Events.Service.IService;

public interface ISpeakersService
{
    Task<Speaker> GetByIdAsync(int id);
    Task<ICollection<GetSpeakerResponse>> GetAllAsync();
}