using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Authorize(Policy = "userPolicy")]
[Route("api/club-challenge-request")]
public class ClubChallengeRequestController : BaseApiController
{
    private readonly IClubChallengeRequestService _requestService;

    public ClubChallengeRequestController(IClubChallengeRequestService requestService)
    {
        _requestService = requestService;
    }
    
    [HttpPost]
    public ActionResult<ClubChallengeRequestDto> Create([FromBody] ClubChallengeRequestDto challengeRequestDto)
    {
        var result = _requestService.Create(challengeRequestDto);
        return CreateResponse(result);
    }
    
    [HttpPut]
    public ActionResult<ClubChallengeRequestDto> Update([FromBody] ClubChallengeRequestDto challengeRequestDto)
    {
        var result = _requestService.Update(challengeRequestDto);
        return CreateResponse(result);
    }
}