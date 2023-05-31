using System.Data;
using Application.Common.DTO;
using Application.Common.Interfaces.Services;
using Application.Extensions;
using Application.Helpers;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static Application.Helpers.Constants;

namespace API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService userService,
            IHttpContextAccessor contextAccessor,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> List()
        {
            try
            {
                

                var users = await _userService.GetUsers();
                // _logger.LogInformation("Retreiving all users");
                _logger.LogDetailedInformation("Retrieving all users", _contextAccessor);

                return Ok(users);

            }
            catch (Exception ex)
            {
                // _logger.LogDetailedError(ex, string.Empty);
                _logger.LogDetailedError(ex, string.Empty, _contextAccessor);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                // _logger.LogInformation("Deleted user with Id: {0}", id);
                _logger.LogDetailedInformation(string.Format("Deleted user with Id: {0}", id), _contextAccessor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, string.Empty);
                _logger.LogDetailedError(ex, string.Empty, _contextAccessor);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            try
            {
                var result = await _userService.UpdateUser(userDTO);
                // _logger.LogInformation("Updated user with email: {0}", userDTO.Email);
                _logger.LogDetailedInformation(string.Format("Updated user with email: {0}", userDTO.Email), _contextAccessor);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, string.Empty);
                _logger.LogDetailedError(ex, string.Empty, _contextAccessor);
                return BadRequest(ex.Message);
            }
        }

       

        
    }
}