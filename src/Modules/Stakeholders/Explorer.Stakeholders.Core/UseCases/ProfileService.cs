using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProfileService: CrudService<PersonDto, Person>, IProfileService, IInternalProfileService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ProfileService(IPersonRepository personRepository, IMapper mapper) : base(personRepository, mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public Result<List<PersonDto>> GetMany(List<int> peopleIds)
        {
            List<PersonDto> people = new();
            foreach (int id in peopleIds)
            {
                var person = _personRepository.Get(id);
                if(person != null)
                {
                    PersonDto newPerson = _mapper.Map<PersonDto>(person);
                    people.Add(newPerson);
                }
            }
            return people;
        }
        public Result<PagedResult<PersonDto>> GetUserNonFollowedProfiles(int page, int pageSize, long userId)
        {
            try
            {
                List<Person> profiles = _personRepository.GetPaged(page, pageSize).Results;
                var user = _personRepository.GetFollowersAndFollowings(userId);

                var nonFollowedProfiles = profiles.Where(profile => !user.Following.Contains(profile) && profile.Id != user.Id).ToList();
                var results = new PagedResult<Person>(nonFollowedProfiles, nonFollowedProfiles.Count);
                return MapToDto(results);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<AccountRegistrationDto> GetProfile(long userId)
        {
            var personProfile = _personRepository.Get(userId);


            var account = new AccountRegistrationDto
            {
                Name = personProfile.Name,
                Surname = personProfile.Surname,
                ProfileImage = personProfile.ProfileImage,
                Biography = personProfile.Biography,
                Quote = personProfile.Quote,
            };

            return Result.Ok(account);
        }

        public Result<PersonDto> UpdateProfile(PersonDto updatedPerson)
        {
            var existingPerson = _personRepository.Get(updatedPerson.Id);

            if (existingPerson == null)
            {
                return Result.Fail(FailureCode.NotFound);
            }

            _mapper.Map(updatedPerson, existingPerson);
            _personRepository.Update(existingPerson);

            var updatedPersonDto = _mapper.Map<PersonDto>(existingPerson);

            return Result.Ok(updatedPersonDto);
        }
        public Result<PagedResult<PersonDto>> GetFollowers(long userId)
        {
            var userProfile = _personRepository.GetFollowersAndFollowings(userId);
            var results = new PagedResult<Person>(userProfile.Followers, userProfile.Followers.Count);
            return MapToDto(results);
        }
        public Result<PagedResult<PersonDto>> GetFollowing(long userId)
        {
            var userProfile = _personRepository.GetFollowersAndFollowings(userId);
            var results = new PagedResult<Person>(userProfile.Following, userProfile.Following.Count);
            return MapToDto(results);
        }
        public Result<PagedResult<PersonDto>> Follow(long followerId, PersonDto followed)
        {
            var follower = _personRepository.GetFollowersAndFollowings(followerId);
            if(follower.IsPersonAlreadyFollowed(followed.Id)) return Result.Fail(FailureCode.Conflict).WithError("You already follow this profile.");

            if(followerId == followed.Id) return Result.Fail(FailureCode.Conflict).WithError("You can't follow yourself");

            follower.Following.Add(_mapper.Map<Person>(followed));
            _personRepository.Update(follower);
            var results = new PagedResult<Person>(follower.Following, follower.Following.Count);

            return MapToDto(results);
        }
        public Result<PagedResult<PersonDto>> Unfollow(long followerId, PersonDto unfollowed)
        {
            var follower = _personRepository.GetFollowersAndFollowings(followerId);
            if(!follower.IsPersonAlreadyFollowed(unfollowed.Id)) return Result.Fail(FailureCode.Conflict).WithError("You don't follow this profile.");

            follower.Following.Remove(_mapper.Map<Person>(unfollowed));
            _personRepository.Update(follower);
            var results = new PagedResult<Person>(follower.Following, follower.Following.Count);

            return MapToDto(results);
        }
    }
}
