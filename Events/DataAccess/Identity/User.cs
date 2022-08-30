using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Events.Models;

public class User:IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual Company Company { get; set; } 
    public int? CompanyId { get; set; }
    public string RoleId { get; set; }
    public virtual Role Role { get; set; } 

}