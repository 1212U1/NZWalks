using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDBContext:IdentityDbContext
    {
        private readonly DbContextOptions dbContext;
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> dbContextOptions):base(dbContextOptions) 
        {
            this.dbContext = dbContextOptions;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> identityRoles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id="a940725c-aed9-4d95-858b-49fa9d9b576e",
                    ConcurrencyStamp="a940725c-aed9-4d95-858b-49fa9d9b576e",
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()

                },
                new IdentityRole
                {
                    Id="a940725c-aed9-4d95-858b-49fa9d9b576f",
                    ConcurrencyStamp="a940725c-aed9-4d95-858b-49fa9d9b576f",
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(identityRoles);
        }
    }
}
