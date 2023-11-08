using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Explorer.API.Controllers.Tourist.TourExecution
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tourexecution/")]
    public class TourLifecycleController : BaseApiController
    {
        private readonly ITourLifecycleService _tourLifecycleService;

        public TourLifecycleController(ITourLifecycleService tourLifecycleService)
        {
            _tourLifecycleService = tourLifecycleService;
        }

        [HttpGet("activeTour")]
        public ActionResult<TourProgressDto> GetActiveByUser()
        {
            var result = _tourLifecycleService.GetActive(ClaimsPrincipalExtensions.PersonId(User));
            return CreateResponse(result);
        }

        [HttpPost("start/{tourId:int}")]
        public ActionResult<TourProgressDto> StartTour([FromRoute] int tourId)
        {
            var result = _tourLifecycleService.StartTour(tourId, ClaimsPrincipalExtensions.PersonId(User));
            return CreateResponse(result);
        }

        [HttpPut("abandonActive")]
        public ActionResult<TourProgressDto> AbandonActiveTour()
        {
            var result = _tourLifecycleService.AbandonActiveTour(ClaimsPrincipalExtensions.PersonId(User));
            return CreateResponse(result);
        }
    }
}
