using API.Domain.Entities;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        string,
        IdentityUserClaim<string>,
        ApplicationUserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>
    >,
    IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        //public ApplicationDbContext(string connectionString) : this(GetOptions(connectionString)) { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.InsertedBy = 1; //TODO: Get the current user id
                        entry.Entity.InsertDateTime = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = 1; //TODO: Get the current user id
                        entry.Entity.UpdateDateTime = DateTime.UtcNow;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            builder.Entity<ApplicationRole>().HasData(
                new IdentityRole
                {
                    Id = "dd39ace1-b160-4674-bfd7-0f8e6342ddb6",
                    ConcurrencyStamp = "091150b9-96fd-4152-b8d4-3de4dfa202d8",
                    Name = "Company",
                    NormalizedName = "COMPANY"
                },
                new IdentityRole
                {
                    Id = "770240c4-7e08-42c9-a2f8-ae036241baf5",
                    ConcurrencyStamp = "a16c39a2-8281-4704-b20d-d4de7ab8b4d3",
                    Name = "Client",
                    NormalizedName = "Client"
                },
                new IdentityRole
                {
                    Id = "58d0254b-0fbc-4a32-a4cf-5b5808bc54ed",
                    ConcurrencyStamp = "cf9e91bd-3d9d-43b7-bc7f-41e2d3eeafc5",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
        }

        //private static DbContextOptions<ApplicationDbContext> GetOptions(string connectionString)
        //{
        //    return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ApplicationDbContext>(), connectionString).Options;
        //}
    }
}