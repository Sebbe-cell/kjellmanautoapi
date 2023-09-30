namespace kjellmanautoapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Inventory> Inventories => Set<Inventory>();
    }
}