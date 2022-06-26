using Microsoft.EntityFrameworkCore;
using Prisma.Data.Models;

namespace Prisma.Data
{
    public class PrismaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=prisma;User Id=sa;Password=4H3Bq^Nna*Z%QGb=;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
