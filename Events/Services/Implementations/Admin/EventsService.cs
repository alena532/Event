using AutoMapper;
using Events.Contracts.Requests.Events;
using Events.Contracts.Responses.Events;
using Events.DBContext.Repositories.Base;
using Events.Models;
using Events.Service.IService;


namespace Events.Service;

public class EventsService:IEventsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Event> _repository;
    private readonly ISpeakersService _speakersService;
    private readonly ICompaniesService _companyService;
    public EventsService(IMapper mapper,IRepository<Event> repository,ISpeakersService speakersService,ICompaniesService companyService)
    {
        _mapper = mapper;
        _repository = repository;
       _speakersService = speakersService;
       _companyService = companyService;
    }

    public async Task<ICollection<GetEventResponse>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        return _mapper.Map<ICollection<GetEventResponse>>(entities);
    }
    
    public async Task<GetEventResponse> GetByIdAsync(int id)
    {

        var entity = await _repository.GetByIdAsync(id);
       // if(entity == null) throw new BadHttpRequestException("Invalid Event Id");
        
        return _mapper.Map<GetEventResponse>(entity);
    }

    public async Task<ICollection<GetEventResponse>> GetByCompanyAsync(int companyId)
    {
        var company = await _companyService.GetByIdAsync(companyId);
        if (company == null) throw new BadHttpRequestException("Invalid Company Id");

        var entities = await _repository.GetAllAsync(
            x => x.CompanyId.Equals(companyId)
        );

        return _mapper.Map<ICollection<GetEventResponse>>(entities);
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        //if(entity == null) throw new BadHttpRequestException("Invalid Event Id");

        await _repository.DeleteAsync(entity);
    }
    
    public async Task<GetEventResponse> CreateAsync(CreateEventRequest request)
    {
        var company = await _companyService.GetByIdAsync(request.CompanyId);
        if (company == null) throw new BadHttpRequestException("Invalid Company Id");

        var speaker = await _speakersService.GetByIdAsync(request.SpeakerId);
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

    public async Task<GetEventResponse> UpdateAsync(int id,EditEventRequest request)
    {
        var entity = await _repository.GetByIdAsync(id);
        //if(entity == null) throw new BadHttpRequestException("Invalid Event Id");
        
        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Plan = request.Plan;
        entity.Time = request.Time;
        entity.Place = request.Place;
        
        Event res = await _repository.UpdateAsync(entity);
        return _mapper.Map<GetEventResponse>(res);

    }

}