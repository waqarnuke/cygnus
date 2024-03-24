using Microsoft.AspNetCore.Identity;

namespace Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } 
        public Address Address { get; set; }
    }
}