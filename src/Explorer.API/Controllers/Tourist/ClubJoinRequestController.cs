using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/clubJoinRequest")]
    public class ClubJoinRequestController: BaseApiController
    {
        private readonly IClubJoinRequestService _requestService;

        public ClubJoinRequestController(IClubJoinRequestService requestService)
        {

            _requestService = requestService;

        }
        [HttpGet]
        public ActionResult<PagedResult<ClubJoinRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {

            var result = _requestService.GetPaged(page, pageSize);
            return CreateResponse(result);

        }

        [HttpPost]
        public ActionResult<ClubJoinRequestDto> Create([FromBody] ClubJoinRequestDto request)
        {
            var result = _requestService.Create(request);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ClubJoinRequestDto> Update([FromBody] ClubJoinRequestDto request)
        {
            var result = _requestService.Update(request);
            return CreateResponse(result);
        }

    }
}
