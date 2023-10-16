using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/users")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<PagedResult<UserDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _userService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        //[HttpPost]
        //public ActionResult<EquipmentDto> Create([FromBody] EquipmentDto equipment)
        //{
        //    var result = _equipmentService.Create(equipment);
        //    return CreateResponse(result);
        //}

        //[HttpPut("{id:int}")]
        //public ActionResult<EquipmentDto> Update([FromBody] EquipmentDto equipment)
        //{
        //    var result = _equipmentService.Update(equipment);
        //    return CreateResponse(result);
        //}

        //[HttpDelete("{id:int}")]
        //public ActionResult Delete(int id)
        //{
        //    var result = _equipmentService.Delete(id);
        //    return CreateResponse(result);
        //}
    }
}
