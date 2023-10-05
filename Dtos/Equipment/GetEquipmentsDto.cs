namespace kjellmanautoapi.Dtos.Equipment
{
    public class GetEquipmentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<GetInventoryDto> Inventories { get; set; }
    }
}