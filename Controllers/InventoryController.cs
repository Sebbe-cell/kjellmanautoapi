using Microsoft.AspNetCore.Mvc;

namespace kjellmanautoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public InventoryController(IInventoryService inventoryService, IWebHostEnvironment hostEnvironment)
        {
            _inventoryService = inventoryService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetInventoryDto>>>> Get()
        {
            return Ok(await _inventoryService.GetAllInventories());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetInventoryDto>>> GetSingle(int id)
        {
            return Ok(await _inventoryService.GetInventoryById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetInventoryDto>>>> AddInventory([FromForm]AddInventoryDto newInventory)
        {
            newInventory.ImageName = await SaveImage(newInventory.ImageFile);
            return Ok(await _inventoryService.AddInventory(newInventory));
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName+DateTime.Now.ToString("yymmssfff")+ Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetInventoryDto>>>> UpdateInventory(UpdateInventoryDto updatedInventory)
        {
            var response = await _inventoryService.UpdateInventory(updatedInventory);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetInventoryDto>>> DeleteInventory(int id)
        {
            var response = await _inventoryService.DeleteInventory(id);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}