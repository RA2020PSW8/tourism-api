using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/keypoints")]
    public class KeypointController : BaseApiController
    {
        private readonly IKeypointService _keypointService;
        
        public KeypointController(IKeypointService keypointService)
        {
            _keypointService = keypointService;
        }

        [HttpGet]
        public ActionResult<PagedResult<KeypointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _keypointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        
        [HttpPost]
        public ActionResult<KeypointDto> Create([FromBody] KeypointDto keypoint)
        {
            var result = _keypointService.Create(keypoint);
            return CreateResponse(result);
        }
        
        [HttpPost("/multiple")]
        public ActionResult<KeypointDto> CreateMultiple([FromBody] List<KeypointDto> keypoints)
        {
            var result = _keypointService.CreateMultiple(keypoints);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<KeypointDto> Update([FromBody] KeypointDto keypoint)
        {
            var result = _keypointService.Update(keypoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _keypointService.Delete(id);
            return CreateResponse(result);
        }
    }
}
