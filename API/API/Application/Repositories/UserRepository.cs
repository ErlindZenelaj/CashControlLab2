using Application.Common.Interfaces.Repositories;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastucture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            var identityUsers = await _userManager.Users.Where(x => x.Deleted == false)
                                                  .Include(user => user.UserRoles)
                                                  .ThenInclude(userRole => userRole.Role)
                                                  .ToListAsync();

            return identityUsers;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public ApplicationUser GetUserByName(string name)
        {
            return _userManager.Users.Where(x => x.UserName == name).FirstOrDefault();
        }
        public ApplicationUser GetUserById(string id)
        {
            return _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<bool> UpdateUser(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserRoles(string userId)
        {
            if (userId == null) return false;

            var applicationUser = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(applicationUser);
            var result = await _userManager.RemoveFromRolesAsync(applicationUser, userRoles);

            return result.Succeeded;
        }


        public List<ApplicationUser> GetUsersByListOfUsernames(List<string> usernames)
        {
            try
            {
                if (usernames == null || usernames.Count == 0)
                {
                    return new List<ApplicationUser>();
                }
                return _userManager.Users
                    .Include(c => c.UserRoles)
                        .ThenInclude(c => c.Role)
                    .Where(x => usernames.Any(y => x.UserName == y)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
