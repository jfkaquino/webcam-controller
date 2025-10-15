using Microsoft.EntityFrameworkCore;
using WebcamController.Models;

namespace WebcamController
{
    public class AppDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Preset> Presets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Application.StartupPath, "data", "app.sqlite");
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath));
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
