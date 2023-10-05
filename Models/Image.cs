namespace kjellmanautoapi.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}