namespace kjellmanautoapi.Dtos.Inventory
{
    public class UpdateInventoryDto
    {
        public int Id { get; set; }
        public string Make { get; set; } = "BMW";
        public string Model { get; set; } = "3-serie";
        public string Color { get; set; } = "Blue";
        public string PlateNumber { get; set; } = "XXX111";
        public int Milage { get; set; } = 10000;
        public int Price { get; set; } = 10000;
    }
}