using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CodeSourceSolution.Models
{
    public class PlayerDbContext:IdentityDbContext
    {
        public PlayerDbContext()
        {
        }

        public PlayerDbContext(DbContextOptions<PlayerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Format>? Formats { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<SeriesEntry>? SeriesEntries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Format>().HasData(
                new Format
                {
                    FormatId =1,
                    FormatName = "ODI"                
                },
                new Format
                {
                    FormatId =2,
                    FormatName = "Test"
                },
                new Format
                {
                    FormatId =3,
                    FormatName = "T20"
                });


          
            base.OnModelCreating(modelBuilder);
            


            modelBuilder.Entity<IdentityUser>().HasKey(m => m.Id);
            
        }
    }
}
