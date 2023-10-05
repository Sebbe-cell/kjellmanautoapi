namespace kjellmanautoapi.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<ServiceResponse<List<GetInventoryDto>>> GetAllInventories();
        Task<ServiceResponse<GetInventoryDto>> GetInventoryById(int id);
        Task<ServiceResponse<GetInventoryDto>> AddInventory(AddInventoryDto newInventory);
        Task<ServiceResponse<GetInventoryDto>> UpdateInventory(UpdateInventoryDto updatedInventory);
        Task<ServiceResponse<List<GetInventoryDto>>> DeleteInventory(int id);
    }
}