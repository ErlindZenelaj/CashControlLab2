using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Common.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<RoleDTO> Roles { get; set; }


        public string OneLoginToken { get; set; }
    }
}