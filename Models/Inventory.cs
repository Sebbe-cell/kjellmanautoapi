namespace kjellmanautoapi.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Header { get; set; } = "";
        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public string Color { get; set; } = "";
        public string PlateNumber { get; set; } = "";
        public int Milage { get; set; } = 0;
        public int ModelYear { get; set; } = 0;
        public string GearBox { get; set; } = "";
        public string Propellent { get; set; } = "";
        public int Price { get; set; } = 0;
        public string Description { get; set; } = "";
        public List<Equipments> Equipment { get; set; }
        public List<Facts> Facts { get; set; }
        public List<Images> Images { get; set; } = new List<Images>();
    }
}