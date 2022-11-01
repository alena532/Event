using Events.Contracts.Responses.Companies;
using Events.Models;

namespace Events.Service.IService;

public interface ICompaniesService
{
    Task<Company> GetByIdAsync(int id);
    Task<ICollection<GetCompanyResponse>> GetAllAsync();
}