namespace kjellmanautoapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<Equipments> Equipments => Set<Equipments>();
        public DbSet<Images> Images => Set<Images>();
        public DbSet<Facts> Facts => Set<Facts>();

    }
}