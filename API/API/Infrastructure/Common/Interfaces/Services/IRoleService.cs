using Application.Common.DTO;

namespace Application.Common.Interfaces.Services
{
    public interface IRoleService
    {
        Task<ResponseDTO<bool>> CreateRole(RoleDTO roleDTO);
        ResponseDTO<List<RoleDTO>> GetRoles();
    }
}