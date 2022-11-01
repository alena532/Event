using AutoMapper;
using Events.Contracts.Responses.Speakers;
using Events.DBContext;
using Events.Models;
using Events.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Events.Service;

public class SpeakersService:ISpeakersService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public SpeakersService(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Speaker> GetByIdAsync(int id)
    {
        return await _context.Speakers.FindAsync(id);
        
    }
    
    public async Task<ICollection<GetSpeakerResponse>> GetAllAsync()
    {
        var entities = await _context.Speakers.ToListAsync();

        return _mapper.Map<ICollection<GetSpeakerResponse>>(entities);
    }
}