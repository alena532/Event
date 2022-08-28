namespace Events.DTO;

public class EventTransferDTO
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Plan { get; set; }
    
    public DateTime Time { get; set; }
    public string Place { get; set; }

    public string Company { get; set; }
    public string Speaker { get; set; }

}