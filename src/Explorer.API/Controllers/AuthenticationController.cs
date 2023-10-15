using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public ActionResult<AuthenticationTokensDto> RegisterTourist([FromBody] AccountRegistrationDto account)
    {
        var result = _authenticationService.RegisterTourist(account);
        return CreateResponse(result);
    }

    [HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);
        return CreateResponse(result);
    }

    [HttpGet("profile")]
    public ActionResult<AccountRegistrationDto> getStakeholderProfile([FromQuery] long userId)
    {
        var profile = _authenticationService.GetPersonProfile(userId);
        if (profile != null)
        {
            return Ok(profile);
        }
        return NotFound();
    }

    [HttpPut("{id:int}")]
    public ActionResult<EquipmentDto> Update(int id,[FromBody] PersonDto person)
    {
        var result = _authenticationService.UpdateProfile(id,person);
        return CreateResponse(result);
    }
}