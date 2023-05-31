using Application.Common.DTO;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System.Net;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public ResponseDTO<List<RoleDTO>> GetRoles()
        {
            try
            {
                var roles = _roleRepository.GetRoles();
                if (roles == null)
                    return new ResponseDTO<List<RoleDTO>>();

                var result = _mapper.Map<List<RoleDTO>>(roles);
                return new ResponseDTO<List<RoleDTO>> { Data = result };
            }
            catch (Exception e)
            {
                var error = new ErrorDTO { Title = "Could not get roles", Message = e.Message };

                return new ResponseDTO<List<RoleDTO>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    Error = error
                };
            }

        }

        public async Task<ResponseDTO<bool>> CreateRole(RoleDTO roleDTO)
        {
            try
            {
                //var applicationRole = _mapper.Map<ApplicationRole>(roleDTO);
                var applicationRole = new ApplicationRole { Name = roleDTO.Name };
                var result = await _roleRepository.CreateRole(applicationRole);

                return new ResponseDTO<bool> { Data = result };
            }
            catch (Exception e)
            {
                var error = new ErrorDTO { Title = "Could not create roles", Message = e.Message };

                return new ResponseDTO<bool>
                {
                    Status = HttpStatusCode.InternalServerError,
                    Error = error
                };
            }
        }
    }
}