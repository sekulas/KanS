using Microsoft.EntityFrameworkCore;

namespace KanS.Entities;

public class KansDbContext : DbContext {

    public KansDbContext(DbContextOptions<KansDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

    }

}