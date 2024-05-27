using Microsoft.EntityFrameworkCore;
using NZWalk.API.Model.Domain;
using System;
using System.Collections.Generic;

namespace NZWalk.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Diffilculty> Diffilculties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed the data for Diffilcuties
            // Easy, medium, hard

            var difficulties = new List<Diffilculty>()
            {
                new Diffilculty()
                {
                    Id = Guid.Parse("e58c00c3-ddd0-4353-91c8-9a13a834c586"),
                    Name = "Easy",
                },
                new Diffilculty()
                {
                    Id = Guid.Parse("e5602235-bb76-411e-a405-db796c5e875f"),
                    Name = "Medium",
                },
                new Diffilculty()
                {
                    Id = Guid.Parse("ee8072e0-0d0f-4db5-bc29-8bac82fede45"),
                    Name = "Hard",
                }
            };

            // Seed difficulties to the database
            modelBuilder.Entity<Diffilculty>().HasData(difficulties);

            // Seed data for Regions    
            var regions = new List<Region>
            {
                new Region()
                {
                    Id = Guid.Parse("9cdc6148-b11c-4b00-ba0e-84a87287e5f7"),
                    Name = "Ha Noi",
                    Code = "100000",
                    RegionImageUrl = "https://photo-cms-baophapluat.epicdn.me/w840/Uploaded/2024/athlraqhpghat/2023_06_25/ho-hoan-kiem-7185.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("98fdcbf9-50d6-4a45-97d6-9244fd266647"),
                    Name = "Da Nang",
                    Code = "550000",
                    RegionImageUrl = "https://tourism.danang.vn/wp-content/uploads/2023/02/tour-du-lich-da-nang-1.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("12b1e65a-36e2-47d3-9392-5e2b26b91acb"),
                    Name = "Sai Gon",
                    Code = "700000",
                    RegionImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcStgxAi3_mA2naEA1Q7zWTSxB26xj6xh3g3piRRG_dvEQ&s"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
