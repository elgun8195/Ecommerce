 using Microsoft.AspNetCore.Identity;

namespace AzureProject.Entity.Concrete
{
    public class AppUser : IdentityUser
    {
        public  string FullName { get; set; } 
        public bool IsRememberMe { get; set; }
        public bool IsActive { get; set; } = true; 
    }
}
