using Events.Models;

namespace Events.Service.IService;

public interface ICompaniesService
{
    Task<Company> GetByIdAsync(int id);
}