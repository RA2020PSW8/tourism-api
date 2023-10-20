using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/clubs")]
    public class ClubController: BaseApiController
    {
        private readonly IClubService _clubService; 

        public ClubController(IClubService clubService) { 
            
            _clubService = clubService;

        }

        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) {
            
            var result = _clubService.GetPaged(page, pageSize);
            return CreateResponse(result);  
        
        }

        [HttpPost]
        public ActionResult<ClubDto> Create([FromBody] ClubDto club)
        {
            var result = _clubService.Create(club);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ClubDto> Update([FromBody] ClubDto club)
        {
            var result = _clubService.Update(club);
            return CreateResponse(result);
        }

    }
}
