using AutoMapper;
using Events.DBContext;
using Events.Models;
using Events.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Events.Service;

public class CheckingService:ICheckingService
{
    
    private readonly AppDbContext _context;
    public CheckingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Company> GetCompanyByIdAsync(int companyId)
    {
        return await _context.Companies.FindAsync(companyId);
    }
    
    public async Task<Speaker> GetSpeakerByIdAsync(int speakerId)
    {
        return await _context.Speakers.FindAsync(speakerId);
        
    }
    
    
    
}