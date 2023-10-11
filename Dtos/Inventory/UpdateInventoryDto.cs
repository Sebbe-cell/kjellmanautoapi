namespace kjellmanautoapi.Dtos.Inventory
{
    public class UpdateInventoryDto
    {
        public int Id { get; set; }
        public string Header { get; set; } = "";
        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public string Color { get; set; } = "";
        public string PlateNumber { get; set; } = "";
        public int Milage { get; set; } = 0;
        public int Price { get; set; } = 0;
        public string Description { get; set; } = "";
        public int ModelYear { get; set; } = 0;
        public string GearBox { get; set; } = "";
        public string Propellent { get; set; } = "";
    }
}