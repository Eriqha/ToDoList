using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models; // Adjust if needed

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Required for Identity

        // User Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
        });

        // Category Configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        // ToDoItem Configuration
        modelBuilder.Entity<ToDoItem>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(t => t.Description)
                .HasMaxLength(500);
            entity.Property(t => t.IsComplete)
                .IsRequired();

            entity.HasOne(t => t.User)
                .WithMany(u => u.ToDoItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Category)
                .WithMany(c => c.ToDoItems)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // SubTask Configuration
        modelBuilder.Entity<SubTask>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(s => s.IsDone)
                .IsRequired();

            entity.HasOne(s => s.ToDoItem)
                .WithMany(t => t.SubTasks)
                .HasForeignKey(s => s.ToDoItemId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
