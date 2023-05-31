using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastucture.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<ApplicationRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<bool> CreateRole(ApplicationRole role)
        {
            var roleResult = await _roleManager.CreateAsync(role);
            return roleResult.Succeeded;
        }
    }
}