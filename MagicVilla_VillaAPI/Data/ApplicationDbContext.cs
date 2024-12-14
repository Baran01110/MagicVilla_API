using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    ID=1,
                    Name="Royal Villa",
                    Details="Deneme metnidir...",
                    ImageUrl="",
                    Occupancy=5,
                    Rate=200,
                    Sqft=550,
                    Amenity="",
                    CreatedDate = DateTime.Now
                },
                new Villa()
                {
                    ID = 2,
                    Name = "Diomand Villa",
                    Details = "Deneme metnidir...",
                    ImageUrl = "",
                    Occupancy = 3,
                    Rate = 400,
                    Sqft = 650,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa()
                {
                    ID = 3,
                    Name = "Gold Villa",
                    Details = "Deneme metnidir...",
                    ImageUrl = "",
                    Occupancy = 4,
                    Rate = 600,
                    Sqft = 750,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa()
                {
                    ID = 4,
                    Name = "Diomand Pool Villa",
                    Details = "Deneme metnidir...",
                    ImageUrl = "",
                    Occupancy = 4,
                    Rate = 800,
                    Sqft = 950,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                });
        }
    }
}
