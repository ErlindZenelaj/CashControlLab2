using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        List<ApplicationRole> GetRoles();
        Task<bool> CreateRole(ApplicationRole role);
    }
}