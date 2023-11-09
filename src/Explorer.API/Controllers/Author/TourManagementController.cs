using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/tours/")]
    public class TourManagementController : BaseApiController
    {
        
        private readonly ITourService _tourService;

        public TourManagementController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("{tourId:int}")]
        public ActionResult<TourDto> GetById([FromRoute] int tourId)
        {
            var result = _tourService.Get(tourId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourDto tour)
        {
            tour.UserId = ClaimsPrincipalExtensions.PersonId(User);
            var result = _tourService.Create(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourDto> Update([FromBody] TourDto tour)
        {
            tour.UserId = ClaimsPrincipalExtensions.PersonId(User);
            var result = _tourService.Update(tour);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("author")]
        public ActionResult<PagedResult<TourDto>> GetByAuthor([FromQuery] int page, [FromQuery] int pageSize)
        {
            var authorId = ClaimsPrincipalExtensions.PersonId(User);
            var result = _tourService.GetByAuthor(page, pageSize, authorId);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpPut("disable/{id:int}")]
        public ActionResult<TourDto> Disable([FromBody] TourDto tour)
        {
            if (User.IsInRole("administrator"))
            {
                tour.UserId = ClaimsPrincipalExtensions.PersonId(User);
                var result = _tourService.Update(tour);
                return CreateResponse(result);
            }
            else
                return null;
        }
    }
}



