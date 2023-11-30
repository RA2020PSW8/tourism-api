using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Route("api/bundles")]
    public class TourBundleController : BaseApiController
    {
        private readonly ITourBundleService _service;

        public TourBundleController(ITourBundleService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<TourBundleDto> Create([FromBody]TourBundleDto bundle)
        {
            var result = _service.Create(bundle);
            return CreateResponse(result);
        }
        [HttpGet]
        public ActionResult<PagedResult<TourBundleDto>> GetPaged([FromQuery] int page, [FromQuery] int pageSize) 
        {
            var result = _service.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourBundleDto> Get(long id)
        {
            var result = _service.Get(id);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<TourBundleDto> Update ([FromBody ]TourBundleDto bundle) 
        {
            var result = _service.Update(bundle);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            return CreateResponse(result);
        }

    }
}
