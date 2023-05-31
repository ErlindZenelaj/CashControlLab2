using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public virtual List<ApplicationUserRole> UserRoles { get; set; }
    }
}