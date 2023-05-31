using Microsoft.EntityFrameworkCore;
using Domain.Common;
using API.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<User>Users { get; set; }

    public DbSet<Company> Companies { get; set; }



}
