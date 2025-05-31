using Microsoft.AspNetCore.Identity;

namespace DAL.Entities;

public class User : IdentityUser
{
    public string Nickname { get; set; }
}