using Microsoft.EntityFrameworkCore;

namespace KanS.Entities;

public class KansDbContext : DbContext {

    public KansDbContext(DbContextOptions<KansDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<UserBoard> UserBoards { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Job> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        // Configure UserBoard entity
        modelBuilder.Entity<UserBoard>()
            .HasKey(ub => ub.AssignmentId);

        modelBuilder.Entity<UserBoard>()
            .Property(ub => ub.Favorite)
            .HasDefaultValue(false);

        modelBuilder.Entity<UserBoard>()
            .Property(ub => ub.AssignmentDate)
            .HasDefaultValue(DateTime.Now.ToUniversalTime());

        modelBuilder.Entity<UserBoard>()
            .Property(ub => ub.Deleted)
            .HasDefaultValue(false);

        modelBuilder.Entity<UserBoard>()
            .Property(ub => ub.FavouritePostion)
            .HasDefaultValue(0);

        modelBuilder.Entity<UserBoard>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBoards)
            .HasForeignKey(ub => ub.UserId);

        modelBuilder.Entity<UserBoard>()
            .HasOne(ub => ub.Board)
            .WithMany(b => b.UserBoards)
            .HasForeignKey(ub => ub.BoardId);

        //Configure Board entity
        modelBuilder.Entity<Board>()
            .Property(j => j.Name)
            .HasDefaultValue("New Board");
        modelBuilder.Entity<Board>()
            .Property(j => j.Description)
            .HasDefaultValue("");
        modelBuilder.Entity<Board>()
            .Property(j => j.Icon)
            .HasDefaultValue("");

        modelBuilder.Entity<Board>()
            .HasMany(b => b.Sections)
            .WithOne(s => s.Board)
            .HasForeignKey(b => b.Id);

        // Configure Section entity
        modelBuilder.Entity<Section>()
            .Property(j => j.Name)
            .HasDefaultValue("New Section");

        modelBuilder.Entity<Section>()
            .HasMany(s => s.Tasks)
            .WithOne(t => t.Section)
            .HasForeignKey(s => s.Id);

        // Configure Job entity
        modelBuilder.Entity<Job>()
            .Property(j => j.Name)
            .HasDefaultValue("New Task");

        modelBuilder.Entity<Job>()
            .Property(j => j.Description)
            .HasDefaultValue("");

    }

}