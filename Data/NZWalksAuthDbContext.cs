using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalk.API.Data
{
    public class NZWalksAuthDbContext :IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options):base  (options) { 
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "daf222c5-bb99-4b82-b766-1e9dfcd1efc0";
            var writerRoleId = "dad326c8-fea2-4f4b-ab71-f5ccf4e21346";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()

                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()

                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
