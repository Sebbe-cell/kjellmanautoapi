using kjellmanautoapi.Services.EquipmentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kjellmanautoapi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet("GetAll"), AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetEquipmentsDto>>>> Get()
        {
            return Ok(await _equipmentService.GetAllEquipments());
        }
    }
}