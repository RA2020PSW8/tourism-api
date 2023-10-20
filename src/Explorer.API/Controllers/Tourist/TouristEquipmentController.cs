using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/touristEquipment")]
    public class TouristEquipmentController : BaseApiController
    {
        private readonly ITouristEquipmentService _touristEquipmentService;
        private readonly IEquipmentService _equipmentService;

        public TouristEquipmentController(ITouristEquipmentService touristEquipmentService, IEquipmentService equipmentService)
        {
            _touristEquipmentService = touristEquipmentService;
            _equipmentService = equipmentService;
        }

        [HttpGet("forSelected/{id:int}")]
        public ActionResult<IEnumerable<EquipmentDto>> GetAllForSelected(int id)
        {
            var result = _equipmentService.GetAll(id);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<TouristEquipmentDto> ItemSelection([FromBody] TouristEquipmentDto touristEquipment)
        {
            var result = _touristEquipmentService.ItemSelection(touristEquipment);
            return CreateResponse(result);
        }

        
    }
}
