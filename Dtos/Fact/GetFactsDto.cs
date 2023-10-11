namespace kjellmanautoapi.Dtos.Equipment
{
    public class GetFactsDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = "";
        public List<GetInventoryDto> Inventories { get; set; }
    }
}