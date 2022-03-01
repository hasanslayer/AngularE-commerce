using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class ApplicationDbUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
        public string Lang { get; set; } = "ar"; 
    }
}