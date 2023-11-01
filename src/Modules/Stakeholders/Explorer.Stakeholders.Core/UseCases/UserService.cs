using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService  : CrudService<UserDto, User>, IUserService, IInternalUserService
    {
        private readonly ICrudRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserService(ICrudRepository<User> repository, IMapper mapper) : base(repository, mapper) 
        {
            _userRepository = repository;
            _mapper = mapper;
        }

        public Result<List<UserDto>> GetMany(List<int> userIds)
        {
            List<UserDto> users = new();
            foreach (int id in userIds)
            {
                var user = _userRepository.Get(id);
                if (user != null)
                {
                    UserDto newUser = new UserDto
                    {
                        Id = (int)user.Id,
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email,
                        IsActive = user.IsActive,
                        IsBlocked = user.IsBlocked,
                        Role = (int)user.Role

                    };
                    users.Add(newUser);
                }
            }
            return users;
        }
    }
}
