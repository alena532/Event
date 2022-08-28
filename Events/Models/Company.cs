using Microsoft.EntityFrameworkCore;

namespace Events.Models;

public class Company
{
    public int Id { get; set; }
    
    
    public string Name { get; set; }
    
    public List<User> Users { get; set; }
    public List<Event> Events { get; set; }
    
    
}