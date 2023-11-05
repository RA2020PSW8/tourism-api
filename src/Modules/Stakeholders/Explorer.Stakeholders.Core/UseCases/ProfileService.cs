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
    public class ProfileService: IProfileService, IInternalProfileService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ProfileService(IPersonRepository personRepository, IMapper mapper) //: base(personRepository, mapper)
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

        public Result<AccountRegistrationDto> GetProfile(long userId)
        {
            var personProfile = _personRepository.Get(userId);


            var account = new AccountRegistrationDto
            {
                Name = personProfile.Name,
                Surname = personProfile.Surname,
                ProfileImage = personProfile.ProfileImage,
                Biography = personProfile.Biography,
                Quote = personProfile.Quote
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
        public bool IsProfileAlreadyFollowed(long follower, long following)
        {
            var existingFollower = _personRepository.Get(follower);
            return existingFollower.Followings.Any(f => f.Id == following);
        }
        public Result<PersonDto> FollowProfile(long follower, long following)
        {
            /*
            var existingFollower = _personRepository.Get(follower);
            var existingFollowing = _personRepository.Get(following);
            */
            /* odmah provjeri da li je moguce bez provjere u controlleru
            if (existingPerson == null)
            {
                return Result.Fail(FailureCode.NotFound);
            }

            existingFollower.Followings.Add(existingFollowing);

            // Update the Followings list for existingFollowing
            existingFollowing.Followers.Add(existingFollower);
            */
            /*_mapper.Map(follower, existingFollower);
            _mapper.Map(following, existingFollowing);

            existingFollower.Followings.Add(following);*/
/*
            _personRepository.Update(existingFollower);
            _personRepository.Update(existingFollowing);

            var updatedFollower = _mapper.Map<PersonDto>(existingFollower);*/
            //var updatedPersonDto = _mapper.Map<PersonDto>(existingPerson);
            var person = _personRepository.GetOne(follower);
            var updatedPersonDto = _mapper.Map<PersonDto>(person);
            return Result.Ok(updatedPersonDto);
        }
    }
}
