namespace kjellmanautoapi.Services.EquipmentService
{
    public interface IEquipmentService
    {
        Task<ServiceResponse<List<GetEquipmentsDto>>> GetAllEquipments();
    }
}