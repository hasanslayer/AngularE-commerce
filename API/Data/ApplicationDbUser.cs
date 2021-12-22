using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class ApplicationDbUser : IdentityUser
    {
        public string Lang { get; set; } = "ar"; 
    }
}