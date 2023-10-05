namespace kjellmanautoapi.Models
{
    public class Equipments
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Inventory> Inventories { get; set; }
    }
}