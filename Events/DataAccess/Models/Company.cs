using Microsoft.EntityFrameworkCore;

namespace Events.Models;

public class Company
{
    public Company()
    {
        Events = new HashSet<Event>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<Event> Events { get; set; }
    
    
}