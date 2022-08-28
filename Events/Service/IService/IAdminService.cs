using Events.DTO;
using Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Service.IService;

public interface IAdminService
{
    public Task<IEnumerable<EventTransferDTO>> GetEvents();
    public Task CreateEvent(EventCreateDTO ev);
    public Task<EventTransferDTO> GetEvent(int eventId);
    public Task DeleteEvent(int id);
}