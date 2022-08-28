
using System.Net;
using System.Web.Http;
using AutoMapper;
using Events.DBContext;
using Events.DTO;
using Events.Models;
using Events.Repository;
using Events.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events.Service;

public class AdminService:IAdminService
{
    private readonly EventsContext _context;
    private readonly IMapper _mapper;
    private readonly AdminRepository _repository;
    public AdminService(EventsContext context,IMapper mapper,AdminRepository repository )
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<IEnumerable<EventTransferDTO>> GetEvents()
    {
        List<EventTransferDTO> listEvents=new();
        var eventsWithCompanySpeaker = await _repository.GetEventsWithCompanySpeaker();
        foreach (var arrangement in eventsWithCompanySpeaker)
        {
            EventTransferDTO arr = _mapper.Map<EventTransferDTO>(arrangement);
            arr.Company = arrangement.Company.Name;
            arr.Speaker = arrangement.Speaker.FirstName;
            listEvents.Add(arr);
        }
        return listEvents;
    }
    public async Task CreateEvent(EventCreateDTO ev)
    {
        var company = await _repository.GetCompany(ev.CompanyId);
        if(company == null) throw new HttpResponseException(HttpStatusCode.NotFound);

        var speaker = await _repository.GetSpeaker(ev.SpeakerId);
        if(speaker == null) throw new HttpResponseException(HttpStatusCode.NotFound);
        
        var arrangement=_mapper.Map<Event>(ev);
        arrangement.Company = company;
        arrangement.Speaker = speaker;
        _repository.CreateEvent(arrangement);
    }
    public async Task<EventTransferDTO> GetEvent(int id)
    {
        var arrangement = await _repository.GetEventWithCompanySpeaker(id);
        if(arrangement == null) throw new HttpResponseException(HttpStatusCode.NotFound);

        var ev = _mapper.Map<EventTransferDTO>(arrangement);
        ev.Company = arrangement.Company.Name;
        ev.Speaker = arrangement.Speaker.FirstName;
        return ev;
    }

    public async Task DeleteEvent(int id)
    {
        var arrangement = await _repository.GetEvent(id);
        if (arrangement == null) throw new HttpResponseException(HttpStatusCode.NotFound);
        
        _repository.DeleteEvent(arrangement);
        _context.SaveChanges();
        
    }
}