using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Deleted { get; set; }

        public string Image { get; set; }

        public string FullName => $"{FirstName} {LastName}";


        public virtual List<ApplicationUserRole> UserRoles { get; set; }

    }
}