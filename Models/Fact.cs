namespace kjellmanautoapi.Models
{
    public class Facts
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = "";
        public List<Inventory> Inventories { get; set; }
    }
}