using Application.Common.DTO;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Extensions;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.Net;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IHttpContextAccessor accessor,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _contextAccessor = accessor;
            _logger = logger;
        }

        public async Task<ResponseDTO<List<UserDTO>>> GetUsers()
        {
            try
            {
                var response = new ResponseDTO<List<UserDTO>>();

                var users = await _userRepository.GetUsers();
                var result = _mapper.Map<List<UserDTO>>(users);

              

                response.Data = result;
                response.Status = HttpStatusCode.OK;
                return response;
            }
            catch (Exception e)
            {
                // _logger.LogError(e, "Error::{Method}() threw an exception", nameof(GetUsers));
                _logger.LogDetailedError(e, $"Error::{nameof(GetUsers)}() threw an exception", _contextAccessor);
                var error = new ErrorDTO { Title = "Users couldn't be loaded", Message = e.Message };

                return new ResponseDTO<List<UserDTO>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    Error = error
                };
            }
        }

        

        public ApplicationUser GetUserByNameAsync(string name)
        {
            try
            {
                var user = _userRepository.GetUserByName(name);

                if (user != null)
                {
                    return user;
                }
                else
                {
                    // _logger.LogInformation("Could not find user with {Name} in {Repository}", name, nameof(IUserRepository));
                    _logger.LogDetailedInformation($"Could not find user with {name} in {nameof(IUserRepository)}", _contextAccessor);
                    return null;
                }

            }
            catch (Exception e)
            {
                // _logger.LogError(e, "Error::{Method}({Name}) threw an exception", nameof(GetUserByNameAsync), name);
                _logger.LogDetailedError(e, $"Error::{nameof(GetUserByNameAsync)}({name}) threw an exception", _contextAccessor);
                return null;
            }
        }
        public ApplicationUser GetUserById(string id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    // _logger.LogInformation("Could not find user with {Id} in {Repository}", id, nameof(IUserRepository));
                    _logger.LogDetailedInformation($"Could not find user with {id} in {nameof(IUserRepository)}", _contextAccessor);
                    return null;
                }

            }
            catch (Exception e)
            {
                // _logger.LogError(e, "Error::{Method}({Id}) threw an exception", nameof(GetUserById), id);
                _logger.LogDetailedError(e, $"Error::{nameof(GetUserById)}({id}) threw an exception", _contextAccessor);
                return null;
            }
        }

        public async Task<ResponseDTO<bool>> DeleteUser(string id)
        {
            try
            {
                var user = await _userRepository.GetUser(id);
                user.Deleted = true;

                var result = await _userRepository.UpdateUser(user);

                return new ResponseDTO<bool> { Data = result };
            }
            catch (Exception e)
            {
                // _logger.LogError(e, "Error::{Method}({Id}) threw an exception", nameof(DeleteUser), id);
                _logger.LogDetailedError(e, $"Error::{nameof(DeleteUser)}({id}) threw an exception", _contextAccessor);
                var error = new ErrorDTO { Title = "User couldn't be deleted", Message = e.Message };

                return new ResponseDTO<bool>
                {
                    Status = HttpStatusCode.InternalServerError,
                    Error = error
                };
            }
        }

        public async Task<ResponseDTO<bool>> UpdateUser(UserDTO userDTO)
        {
            try
            {
                //userPropertyService.deleteUserProperties(userDTO.Id);
                var user = await _userRepository.GetUser(userDTO.Id);
                var applicationUser = _mapper.Map(userDTO, user);
                var deleteRolesResult = await _userRepository.DeleteUserRoles(userDTO.Id);
                if (deleteRolesResult == false)
                    throw new Exception();


                //List<UserProperty> distinctUserProperty = new List<UserProperty>();
                //foreach (var item in userPropertyies)
                //{
                //    if (!distinctUserProperty.Any(x=> x.PropertyId == item.PropertyId))
                //    {
                //        distinctUserProperty.Add(new UserProperty { UserId = userDTO.Id, PropertyId = item.PropertyId, InsertedBy = 1, InsertDateTime = DateTime.Now });
                //    }
                //}
                //applicationUser.UserProperties = null;//distinctUserProperty;

                var result = await _userRepository.UpdateUser(applicationUser);

                // this.userPropertyService.AddUserProperties(distinctUserProperty);
                //this.userPropertyService.AddUserProperties(applicationUser.UserProperties);
                return new ResponseDTO<bool>
                {
                    Status = HttpStatusCode.OK,
                    Data = result
                };

            }
            catch (Exception e)
            {
                // _logger.LogError(e, "Error::{Method}({User}) threw an exception", nameof(UpdateUser), userDTO.ToJson());
                _logger.LogDetailedError(e, $"Error::{nameof(UpdateUser)}({userDTO.ToJson()}) threw an exception", _contextAccessor);
                var error = new ErrorDTO { Title = "User couldn't be deleted", Message = e.Message };

                return new ResponseDTO<bool>
                {
                    Status = HttpStatusCode.InternalServerError,
                    Error = error
                };
            }
        }



    }
}