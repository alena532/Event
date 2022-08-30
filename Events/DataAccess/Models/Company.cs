using Microsoft.EntityFrameworkCore;

namespace Events.Models;

public class Company
{
    public Company()
    {
        Users = new HashSet<User>();
        Events = new HashSet<Event>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Event> Events { get; set; }
    
    
}