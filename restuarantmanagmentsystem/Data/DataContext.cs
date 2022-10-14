

namespace restuarantmanagmentsystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>().HasData(
                new Staff
                {
                    Id = 1,
                    Name = "George",
                    Passcode = 6060
                },
                new Staff
                {
                    Id = 2,
                    Name = "Lisa",
                    Passcode = 9000
                },
                new Staff
                {
                    Id = 3,
                    Name = "Tom",
                    Passcode = 2222
                }
                );

            modelBuilder.Entity<Menu>().HasData(
                new Menu
                {
                    ID = 1,
                    Name = "CheeseBurger",
                    Price = 8.00f,
                    CategoryType = 2
                },
                new Menu
                {
                    ID = 2,
                    Name = "Pizza",
                    Price = 6.00f,
                    CategoryType = 2
                },
                new Menu
                {
                    ID = 3,
                    Name = "Ice Cream",
                    Price = 3.00f,
                    CategoryType = 3

                }
                );
            modelBuilder.Entity<Table>().HasData(
                new Table
                {
                    Id = 1,
                    TableNumber = 100,
                    IsAvailable = true,
                },
                 new Table
                 {
                     Id = 2,
                     TableNumber = 101,
                     IsAvailable = true,
                 },
                new Table
                {
                    Id = 3,
                    TableNumber = 103,
                    IsAvailable = true,
                }
                );


        }



        public DbSet<Staff> Staff { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<DayRevenue> DayRevenue { get; set; }
        public DbSet<Table> Table { get; set; }

    }
}
