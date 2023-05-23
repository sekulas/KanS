using Microsoft.EntityFrameworkCore;

namespace KanS.Entities;

public class KansDbContext : DbContext {

    public KansDbContext(DbContextOptions<KansDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<UserBoard> UserBoards { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<TaskE> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.BoardsCreated)
            .HasDefaultValue(0);

        // Configure UserBoard entity
        modelBuilder.Entity<UserBoard>()
            .HasKey(ub => ub.AssignmentId);

        modelBuilder.Entity<UserBoard>()
            .Property(ub => ub.Deleted)
            .HasDefaultValue(false);

        modelBuilder.Entity<UserBoard>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBoards)
            .HasForeignKey(ub => ub.UserId);

        modelBuilder.Entity<UserBoard>()
            .HasOne(ub => ub.Board)
            .WithMany(b => b.UserBoards)
            .HasForeignKey(ub => ub.BoardId);

        modelBuilder.Entity<UserBoard>()
            .Property(ub => ub.ParticipatingAccepted)
            .HasDefaultValue("pending");

        //Configure Board entity
        modelBuilder.Entity<Board>()
            .Property(j => j.Name)
            .HasDefaultValue("New Board");
        modelBuilder.Entity<Board>()
            .Property(j => j.Description)
            .HasDefaultValue("");
        modelBuilder.Entity<Board>()
            .Property(ub => ub.Favourite)
            .HasDefaultValue(false);

        modelBuilder.Entity<Board>()
            .HasMany(b => b.Sections)
            .WithOne(s => s.Board)
            .HasForeignKey(b => b.BoardId);

        // Configure Section entity
        modelBuilder.Entity<Section>()
            .Property(j => j.Name)
            .HasDefaultValue("New Section");

        modelBuilder.Entity<Section>()
            .HasMany(s => s.Tasks)
            .WithOne(t => t.Section)
            .HasForeignKey(s => s.SectionId);

        modelBuilder.Entity<Section>()
            .Property(s => s.Deleted)
            .HasDefaultValue(false);

        // Configure Task entity
        modelBuilder.Entity<TaskE>()
            .Property(j => j.Name)
            .HasDefaultValue("");

        modelBuilder.Entity<TaskE>()
            .Property(j => j.AssignedTo)
            .HasDefaultValue("");

        modelBuilder.Entity<TaskE>()
            .Property(j => j.Deleted)
            .HasDefaultValue(false);

    }

}