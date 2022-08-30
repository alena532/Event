namespace Events.Contracts.Responses.Users;

public class GetCompanyWorkerResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Company { get; set; }
}