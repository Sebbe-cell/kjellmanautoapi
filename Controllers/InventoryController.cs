using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kjellmanautoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public InventoryController(IInventoryService inventoryService, IWebHostEnvironment hostEnvironment)
        {
            _inventoryService = inventoryService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("GetAll"), AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetInventoryDto>>>> Get()
        {
            return Ok(await _inventoryService.GetAllInventories());
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<GetInventoryDto>>> GetSingle(int id)
        {
            return Ok(await _inventoryService.GetInventoryById(id));
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<GetInventoryDto>> AddInventory([FromForm] AddInventoryDto newInventory)
        {
            if (newInventory == null || newInventory.ImageFile == null)
            {
                return BadRequest("Invalid input"); // Return a 400 Bad Request with an error message for invalid input
            }

            newInventory.ImageName = await SaveImage(newInventory.ImageFile);
            var serviceResponse = await _inventoryService.AddInventory(newInventory);

            if (serviceResponse.Success)
            {
                return Ok(serviceResponse.Data); // Return the newly created object in the response
            }
            else
            {
                return BadRequest(serviceResponse.Message); // Return a 400 Bad Request with an error message if there was an error
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
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