using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class ApplicationContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Mod> Mods { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ModVersion> ModVersions { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new BookMap(modelBuilder.Entity<Book>());
        new ModMap(modelBuilder.Entity<Mod>());
        new TagMap(modelBuilder.Entity<Tag>());
        new VersionMap(modelBuilder.Entity<ModVersion>());
    }
}