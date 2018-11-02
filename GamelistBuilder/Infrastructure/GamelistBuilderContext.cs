using GamelistBuilder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Infrastructure
{
    public class GamelistBuilderContext : DbContext
    {
        public DbSet<Gamelist> Gamelists { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameFolder> Folders { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<FileExtensions> FileExtensions { get; set; }

        public IConfiguration _configuration;

        public GamelistBuilderContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
           
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Game>().HasOne(p => p.Gamelist).WithMany(p => p.Games).IsRequired();
           // modelBuilder.Entity<Game>().HasOne(p => p.Game).WithMany(p => p.).IsRequired();

            //modelBuilder.Entity<GameFolder>().HasOne(p => p.Gamelist).WithMany(f => f.Games).IsRequired();

        }
    }
}
