using Events.Contracts.Requests.Events;
using Events.Contracts.Responses.Events;
using Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Service.IService;

public interface IEventsService
{
    public Task<ICollection<GetEventResponse>> GetAllAsync();
    public Task<ICollection<GetEventResponse>> GetByCompanyAsync(int companyId);
    public Task DeleteAsync(int eventId);
    public Task<GetEventResponse> CreateAsync(CreateEventRequest request);

    public  Task<ICollection<GetEventResponse>> GetByIdAsync(int eventId);
    //public Task UpdateEventAsync(int eventId);
}