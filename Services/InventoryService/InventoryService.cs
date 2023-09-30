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

        public async Task<ServiceResponse<List<GetInventoryDto>>> AddInventory(AddInventoryDto newInventory)
        {
            var serviceResponse = new ServiceResponse<List<GetInventoryDto>>();
            try
            {
                var inventory = _mapper.Map<Inventory>(newInventory);

                // Add the new inventory to the context
                _context.Inventories.Add(inventory);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Retrieve and map all inventories from the context
                var dbInventories = await _context.Inventories.ToListAsync();
                serviceResponse.Data = dbInventories.Select(i => _mapper.Map<GetInventoryDto>(i)).ToList();
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
            var dbInventory = await _context.Inventories.ToListAsync();

            serviceResponse.Data = dbInventory.Select(i => _mapper.Map<GetInventoryDto>(i)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetInventoryDto>> GetInventoryById(int id)
        {
            var serviceResponse = new ServiceResponse<GetInventoryDto>();
            var dbInventory = await _context.Inventories.FirstOrDefaultAsync(i => i.Id == id);
            serviceResponse.Data = _mapper.Map<GetInventoryDto>(dbInventory);
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