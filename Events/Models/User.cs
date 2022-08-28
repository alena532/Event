using Microsoft.AspNetCore.Identity;

namespace Events.Models;

public class User:IdentityUser
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual Company Company { get; set; } = null!;
    public int? CompanyId { get; set; }

}