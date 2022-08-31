namespace Events.Contracts.Requests.Events;

public class EditEventRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Plan { get; set; }
    public DateTime Time { get; set; }
    public string Place { get; set; }
}