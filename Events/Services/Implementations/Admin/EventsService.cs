using AutoMapper;
using Events.Contracts.Requests.Events;
using Events.Contracts.Responses.Events;
using Events.DBContext;
using Events.DBContext.Repositories.Base;
using Events.Models;
using Events.Service.IService;


namespace Events.Service;

public class EventsService:IEventsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Event> _repository;
    private readonly ICheckingService _usersService;
    public EventsService(IMapper mapper,IRepository<Event> repository,ICheckingService usersService)
    {
        _mapper = mapper;
        _repository = repository;
       _usersService = usersService;
    }

    public async Task<ICollection<GetEventResponse>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        return _mapper.Map<ICollection<GetEventResponse>>(entities);
        
    }
    
    public async Task<GetEventResponse> GetByIdAsync(int eventId)
    {
        var entity = await _repository.GetByIdAsync(eventId);

        return _mapper.Map<GetEventResponse>(entity);
    }

    public async Task<ICollection<GetEventResponse>> GetByCompanyAsync(int companyId)
    {
        if (companyId <= 0) throw new BadHttpRequestException("Invalid Organization Id");

        var entities = await _repository.GetAllAsync(
            x => x.CompanyId == companyId
        );

        return _mapper.Map<ICollection<GetEventResponse>>(entities);
    }
    public async Task DeleteAsync(int eventId)
    {
        if (eventId <= 0) throw new BadHttpRequestException("Invalid Company Id");
        Event res = await _repository.GetByIdAsync(eventId);
        
        if (res == null) throw new BadHttpRequestException("Invalid Event Id");
        await _repository.DeleteAsync(res);
    }
    
    public async Task<GetEventResponse> CreateAsync(CreateEventRequest request)
    {
        if (request.CompanyId <= 0 || request.SpeakerId <= 0) throw new BadHttpRequestException("Invalid Organization Id");

        var company = await _usersService.GetCompanyByCompanyIdAsync(request.CompanyId);
        if (company == null) throw new BadHttpRequestException("Invalid Company Id");

        var speaker = await _usersService.GetSpeakerBySpeakerIdAsync(request.SpeakerId);
        if (speaker == null) throw new BadHttpRequestException("Invalid Speaker Id");

        var entity = new Event()
        {
            Title = request.Title,
            Description = request.Description,
            Plan = request.Plan,
            Company = company,
            Speaker = speaker,
            Time = request.Time,
            Place = request.Place

        };
        Event res = await _repository.AddAsync(entity);
        return _mapper.Map<GetEventResponse>(res);
    }

    public async Task UpdateAsync(int id,EditEventRequest request)
    {
        Event ev =await _repository.GetByIdAsync(id);
        var company = await _usersService.GetCompanyByCompanyIdAsync(request.CompanyId);
        if (company == null) throw new BadHttpRequestException("Invalid Company Id");

        var speaker = await _usersService.GetSpeakerBySpeakerIdAsync(request.SpeakerId);
        if (speaker == null) throw new BadHttpRequestException("Invalid Speaker Id");


        ev.Title = request.Title;
        ev.Description = request.Description;
        ev.Plan = request.Plan;
        ev.Company = company;
        ev.Speaker = speaker;
        ev.Time = request.Time;
        ev.Place = request.Place;
        await _repository.UpdateAsync(ev);




    }

    
}