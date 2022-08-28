namespace Events.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Plan { get; set; }
    
    public virtual Company Company { get; set; }
    public int CompanyId { get; set; }
    
    public virtual Speaker Speaker { get; set; }
    public int SpeakerId { get; set; }

    public DateTime Time { get; set; }
    public string Place { get; set; }
}