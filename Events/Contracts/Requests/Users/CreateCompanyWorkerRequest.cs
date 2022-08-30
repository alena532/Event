namespace Events.Contracts.Requests.Users;

public class CreateCompanyWorkerRequest
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public int CompanyId { get; set; }
}