using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IAuthenticationService
{
    Result<AuthenticationTokensDto> Login(CredentialsDto credentials);
    Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account);

    Result<AccountRegistrationDto> GetPersonProfile(long userId);
    Result<PersonDto> Update(PersonDto updatedPerson);
    

}