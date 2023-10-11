using kjellmanautoapi.Services.EquipmentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kjellmanautoapi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class FactsController : Controller
    {
        private readonly IEquipmentService _equipmentService;

        public FactsController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetFactsDto>>>> Get()
        {
            return Ok(await _equipmentService.GetAllEquipments());
        }
    }
}