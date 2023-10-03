namespace kjellmanautoapi.Dtos.Inventory
{
    public class AddInventoryDto
    {
        public string Make { get; set; } = "BMW";
        public string Model { get; set; } = "3-serie";
        public string Color { get; set; } = "Blue";
        public string PlateNumber { get; set; } = "XXX111";
        public int Milage { get; set; } = 10000;
        public int Price { get; set; } = 10000;
        public string ImageName { get; set; } = "";
        public string Description { get; set; } = "";
        public IFormFile? ImageFile { get; set; }
    }
}