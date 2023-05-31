using Application.Common.DTO;
using Domain.Entities;

namespace Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResponseDTO<bool>> DeleteUser(string id);

        ApplicationUser GetUserById(string id);

        ApplicationUser GetUserByNameAsync(string name);

        Task<ResponseDTO<List<UserDTO>>> GetUsers();

        Task<ResponseDTO<bool>> UpdateUser(UserDTO userDTO);

   
    }
}