using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Data
{
    public class ApplicationDbUser : IdentityUser
    {
        public string Lang { get; set; } = "ar"; 
    }
}