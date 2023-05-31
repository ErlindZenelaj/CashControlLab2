using Domain.Common;
using Domain.Entities;



namespace Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUsers();

        Task<bool> DeleteUser(string id);

        Task<ApplicationUser> GetUser(string id);

        Task<bool> UpdateUser(ApplicationUser user);

        Task<bool> DeleteUserRoles(string userId);

        ApplicationUser GetUserByName(string name);

        ApplicationUser GetUserById(string id);

        List<ApplicationUser> GetUsersByListOfUsernames(List<string> usernames);
    }
}