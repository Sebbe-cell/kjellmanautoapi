namespace kjellmanautoapi.Services.InventoryService
{
    public class InventoryService : IInventoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public InventoryService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<ServiceResponse<GetInventoryDto>> AddInventory(AddInventoryDto newInventory)
        {
            var serviceResponse = new ServiceResponse<GetInventoryDto>(); // Change the response type to GetInventoryDto
            try
            {
                var inventory = _mapper.Map<Inventory>(newInventory);

                var selectedEquipment = _context.Equipments.Where(e => newInventory.EquipmentIds.Contains(e.Id)).ToList();

                // Associate the selected equipment with the inventory
                inventory.Equipment = selectedEquipment;
                // Add the new inventory to the context
                _context.Inventories.Add(inventory);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Map and return the newly created inventory
                serviceResponse.Data = _mapper.Map<GetInventoryDto>(inventory);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetInventoryDto>>> DeleteInventory(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetInventoryDto>>();

            try
            {
                var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.Id == id);
                if (inventory is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Inventory with Id '{id}' not found";
                }
                else
                {
                    // Remove the inventory from the context
                    _context.Inventories.Remove(inventory);
                    await _context.SaveChangesAsync();

                    // Retrieve and map all remaining inventories from the context
                    var dbInventories = await _context.Inventories.ToListAsync();
                    serviceResponse.Data = dbInventories.Select(i => _mapper.Map<GetInventoryDto>(i)).ToList();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetInventoryDto>>> GetAllInventories()
        {
            var serviceResponse = new ServiceResponse<List<GetInventoryDto>>();
            var dbInventories = await _context.Inventories
                .Include(i => i.Equipment) // Include the related equipment
                .ToListAsync();

            serviceResponse.Data = dbInventories.Select(i =>
            {
                var inventoryDto = _mapper.Map<GetInventoryDto>(i);
                inventoryDto.ImageSrc = GetImageSrc(i.ImageName);
                return inventoryDto;
            }).ToList();

            return serviceResponse;
        }

        private string GetImageSrc(string imageName)
        {
            var baseUrl = "https://localhost:7114/Images/";
            var imageUrl = baseUrl + imageName;
            return imageUrl;
        }

        public async Task<ServiceResponse<GetInventoryDto>> GetInventoryById(int id)
        {
            var serviceResponse = new ServiceResponse<GetInventoryDto>();
            var dbInventory = await _context.Inventories
            .Include(i => i.Equipment)
            .FirstOrDefaultAsync(i => i.Id == id);

            if (dbInventory == null)
            {
                serviceResponse.Message = "Inventory not found.";
                return serviceResponse;
            }

            var inventoryDto = _mapper.Map<GetInventoryDto>(dbInventory);
            inventoryDto.ImageSrc = GetImageSrc(dbInventory.ImageName); // Set the ImageSrc property
            serviceResponse.Data = inventoryDto;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetInventoryDto>> UpdateInventory(UpdateInventoryDto updatedInventory)
        {
            var serviceResponse = new ServiceResponse<GetInventoryDto>();

            try
            {
                var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.Id == updatedInventory.Id);
                if (inventory is null)
                {
                    throw new Exception($"Inventory with Id '{updatedInventory.Id}' not found");
                }

                inventory.Model = updatedInventory.Model;
                inventory.Make = updatedInventory.Make;
                inventory.Milage = updatedInventory.Milage;
                inventory.Price = updatedInventory.Price;
                inventory.PlateNumber = updatedInventory.PlateNumber;
                inventory.Color = updatedInventory.Color;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetInventoryDto>(inventory);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}