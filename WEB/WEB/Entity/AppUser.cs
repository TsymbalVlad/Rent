using Microsoft.AspNetCore.Identity;

namespace WEB.Entity
{
    public class AppUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
    }
}
