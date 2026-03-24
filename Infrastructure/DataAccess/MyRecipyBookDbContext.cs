using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class MyRecipyBookDbContext : DbContext
{
    public MyRecipyBookDbContext(DbContextOptions options) :base(options) {}
    
    public DbSet<User> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipyBookDbContext).Assembly);
    }
}
