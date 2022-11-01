using System.Linq.Expressions;
using Events.DBContext.Repositories.Base;
using Events.Models;
using HotChocolate;

namespace Events.DBContext;

public class Query
{
    public async Task<IReadOnlyList<Event>> AllEventsAsync([Service] IRepository<Event> eventRepository)
        => await eventRepository.GetAllAsync();

    public async Task<Event> GetByEventIdAsync([Service] IRepository<Event> eventRepository, int id)
    {
        Event getEvent = await eventRepository.GetByIdAsync(id);
        return getEvent;
    }
        

}