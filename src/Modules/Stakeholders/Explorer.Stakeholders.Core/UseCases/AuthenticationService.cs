﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Diagnostics;

namespace Explorer.Stakeholders.Core.UseCases;

public class AuthenticationService : CrudService<PersonDto,Person>, IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ICrudRepository<Person> _personRepository;
    private readonly IMapper _mapper;

    public AuthenticationService(IUserRepository userRepository, ICrudRepository<Person> personRepository, ITokenGenerator tokenGenerator, IMapper mapper): base (personRepository,mapper)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public Result<AuthenticationTokensDto> Login(CredentialsDto credentials)
    {
        var user = _userRepository.GetActiveByName(credentials.Username);
        if (user == null || credentials.Password != user.Password) return Result.Fail(FailureCode.NotFound);

        long personId;
        try
        {
            personId = _userRepository.GetPersonId(user.Id);
            var personProfile = GetPersonProfile(personId);
        }
        catch (KeyNotFoundException)
        {
            personId = 0;
        }

        return _tokenGenerator.GenerateAccessToken(user, personId);
    }

   

    public Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account)
    {
        if(_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

        try
        {
            var user = _userRepository.Create(new User(account.Username, account.Password, UserRole.Tourist, true));
            var person = _personRepository.Create(new Person(user.Id, account.Name, account.Surname, account.Email, account.ProfileImage, account.Biography, account.Quote));

            return _tokenGenerator.GenerateAccessToken(user, person.Id);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            // There is a subtle issue here. Can you find it?
        }
    }

    public AccountRegistrationDto GetPersonProfile(long userId)
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

        return account;
    }


    public Result<PersonDto> Update(PersonDto updatedPerson)
    {
        // Retrieve the existing Person entity by its ID or some other identifier
        var existingPerson = _personRepository.Get(updatedPerson.Id);

        if (existingPerson == null)
        {
            return Result.Fail(FailureCode.NotFound);
        }

        // Map the updatedPerson DTO to the existing Person entity
        _mapper.Map(updatedPerson, existingPerson);

        // Update the entity in the repository
        _personRepository.Update(existingPerson);

        // Map the updated entity back to a DTO
        var updatedPersonDto = _mapper.Map<PersonDto>(existingPerson);

        return Result.Ok(updatedPersonDto);
    }

}







