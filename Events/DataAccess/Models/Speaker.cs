namespace Events.Models;

public class Speaker
{
    public Speaker()
    {
        Events = new HashSet<Event>();
    }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Event> Events { get; set; }
}