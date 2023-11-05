﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/publicEntityRequests")]
    public class PublicEntityRequestController : BaseApiController
    {
        private readonly IPublicEntityRequestService _publicEntityRequestService;

        public PublicEntityRequestController(IPublicEntityRequestService publicEntityRequestService)
        {
            _publicEntityRequestService = publicEntityRequestService;
        }

        [HttpGet]
        public ActionResult<PagedResult<PublicEntityRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) //TODO: get only with pending status or on frontend
        {
            var result = _publicEntityRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PublicEntityRequestDto> Get(int id)
        {
            var result = _publicEntityRequestService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<PublicEntityRequestDto> Create([FromBody] PublicEntityRequestDto publicEntityRequestDto)
        {
            var result = _publicEntityRequestService.Create(publicEntityRequestDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)  // za cancel
        {
            var result = _publicEntityRequestService.Delete(id);
            return  CreateResponse(result);
        }
    }
}
