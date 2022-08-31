using Events.Contracts.Requests.Users;
using Events.Contracts.Responses.Users;
using Events.Models;

namespace Events.Service.IService;

public interface ICheckingService
{
    Task<GetAdminResponse> CreateAdminAsync(CreateAdminRequest request);
    Task<Company> GetCompanyByIdAsync(int companyId);
    Task<Speaker> GetSpeakerByIdAsync(int speakerId);
    Task<Event> GetEventByIdAsync(int eventId);

}