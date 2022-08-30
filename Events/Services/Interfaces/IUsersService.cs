using Events.Contracts.Requests.Users;
using Events.Contracts.Responses.Users;
using Events.Models;

namespace Events.Service.IService;

public interface IUsersService
{
    Task<GetAdminResponse> CreateAdminAsync(CreateAdminRequest request);
    Task<GetCompanyWorkerResponse> CreateCompanyWorkerAsync(CreateCompanyWorkerRequest request);
    Task<ICollection<GetCompanyWorkerResponse>> GetCompanyWorkersAsync();
    Task<Company> GetCompanyByCompanyIdAsync(int companyId);
    Task<Speaker> GetSpeakerBySpeakerIdAsync(int speakerId);

}