namespace Events.DTO;

public class EventCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Plan { get; set; }
    
    public int SpeakerId { get; set; }
    public int CompanyId { get; set; }

    public DateTime Time { get; set; }
    public string Place { get; set; }
}