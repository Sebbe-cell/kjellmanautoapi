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
        private readonly DataContext _context;

        public InventoryController(IInventoryService inventoryService, IWebHostEnvironment hostEnvironment, DataContext context)
        {
            _inventoryService = inventoryService;
            _hostEnvironment = hostEnvironment;
            _context = context;
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

        [HttpPost]
        public async Task<ActionResult<GetInventoryDto>> AddInventory([FromForm] AddInventoryDto newInventory)
        {
            try
            {
                var inventory = new Inventory
                {
                    Make = newInventory.Make,
                    Model = newInventory.Model,
                    Color = newInventory.Color,
                    Milage = newInventory.Milage,
                    Price = newInventory.Price,
                    PlateNumber = newInventory.PlateNumber,
                    Description = newInventory.Description,
                };

                // Add the equipment associations
                if (newInventory.EquipmentIds != null && newInventory.EquipmentIds.Count > 0)
                {
                    var selectedEquipment = _context.Equipments.Where(e => newInventory.EquipmentIds.Contains(e.Id)).ToList();
                    inventory.Equipment = selectedEquipment;
                }

                // Add the inventory to the context and save changes to generate the InventoryId
                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();

                if (newInventory.Images != null && newInventory.Images.Count > 0)
                {
                    foreach (var formFile in newInventory.Images)
                    {
                        if (formFile.Length > 0)
                        {
                            // Generate a unique GUID for the image filename
                            var imageGuid = Guid.NewGuid();

                            // Construct the image filename with the GUID and extension
                            var imageName = imageGuid.ToString() + Path.GetExtension(formFile.FileName);
                            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);

                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }

                            var image = new Images
                            {
                                FileName = imageName, // Use the filename with the GUID
                                InventoryId = inventory.Id
                            };

                            // Add the image to the context
                            _context.Images.Add(image);
                        }
                    }

                    // Save changes again after adding images
                    await _context.SaveChangesAsync();
                }

                return Ok("Inventory and images added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
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