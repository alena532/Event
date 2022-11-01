using Events.DBContext.Repositories.Base;
using Events.Models;
using Events.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Events.DBContext;

public class Mutation
{
    
    public async Task<Event> AddEventAsync(CreateInput input, [Service] IRepository<Event> eventRepository,[Service] ICompaniesService companyService,[Service] ISpeakersService speakerService)
    {
        var company = await companyService.GetByIdAsync(input.companyId) ?? throw new Exception("Company not found");
        var speaker = await speakerService.GetByIdAsync(input.speakerId) ?? throw new Exception("Company not found");
        Event addEvent = new()
        {
            Title = input.title,
            Description = input.description,
            Plan = input.plan,
            Time = input.time,
            Place = input.place,
            Speaker = speaker,
            Company = company


        };
        var addedEvent = await eventRepository.AddAsync(addEvent);
        return addedEvent;
    }
    public record CreateInput(int speakerId, int companyId,string title,string description,string plan,DateTime time,string place );
    
    
}