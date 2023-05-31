using Application.Common.DTO;
using Application.Common.Interfaces.Services;
using Application.Extensions;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RolesController> _logger;
        private readonly IRoleService _roleService;
        private readonly IHttpContextAccessor _contextAccessor;

        public RolesController(IRoleService roleService, IHttpContextAccessor contextAccessor,
            RoleManager<ApplicationRole> roleManager, ILogger<RolesController> logger)
        {
            _roleService = roleService;
            _contextAccessor = contextAccessor;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            try
            {
                var result = _roleService.GetRoles();
                // _logger.LogInformation("Retrieving all roles");
                _logger.LogDetailedInformation("Retrieving all roles", _contextAccessor);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, string.Empty);
                _logger.LogDetailedError(ex, string.Empty, _contextAccessor);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] RoleDTO roleDto)
        {
            try
            {
                var role = new ApplicationRole { Name = roleDto.Name };
                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    var message = string.Join(", ", result.Errors
                        .Select(c => string.Format("Code: {0} - Description: {1}", c.Code, c.Description)));
                    // _logger.LogError("Failed to create role with name: {0}. Error: {1}", roleDto.Name, message);
                    _logger.LogDetailedError(new Exception(),
                        string.Format("Failed to create role with name: {0}. Error: {1}",
                            roleDto.Name, message),
                        _contextAccessor);

                    return BadRequest();
                }

                // _logger.LogInformation("Succesfuly created role with name: {0}", roleDto.Name);
                _logger.LogDetailedInformation(string.Format("Succesfuly created role with name: {0}", roleDto.Name), _contextAccessor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, string.Empty);
                _logger.LogDetailedError(ex, string.Empty, _contextAccessor);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromBody] RoleDTO roleDto)
        {
            try
            {
                //TODO - check if this role exists in AspNetUserRoles
                var role = await _roleManager.FindByNameAsync(roleDto.Name);
                var roleResult = await _roleManager.DeleteAsync(role);

                if (!roleResult.Succeeded)
                {
                    var message = string.Join(", ", roleResult.Errors
                       .Select(c => string.Format("Code: {0} - Description: {1}", c.Code, c.Description)));
                    // _logger.LogError("Failed to delete role with name: {0}. Error: {1}", roleDto.Name, message);
                    _logger.LogDetailedError(new Exception(),
                        string.Format("Failed to delete role with name: {0}. Error: {1}",
                            roleDto.Name, message),
                        _contextAccessor);

                    return BadRequest();
                }

                // _logger.LogInformation("Succesfuly deleted role with name: {0}", roleDto.Name);
                _logger.LogDetailedInformation(string.Format("Succesfuly deleted role with name: {0}", roleDto.Name), _contextAccessor);
                return Ok();
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