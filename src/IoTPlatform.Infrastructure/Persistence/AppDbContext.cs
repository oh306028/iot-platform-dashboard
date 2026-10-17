using IoTPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public DbSet<Alert> Alerts => Set<Alert>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Username)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasIndex(u => u.Username)
                  .IsUnique();

            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.HasIndex(u => u.Email)
                  .IsUnique();

            entity.Property(u => u.PasswordHash)
                  .IsRequired()
                  .HasMaxLength(512);

            entity.Property(u => u.Role)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(u => u.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(d => d.ClientIdentifier)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.HasIndex(d => d.ClientIdentifier)
                  .IsUnique();

            entity.Property(d => d.Description)
                  .HasMaxLength(1000);

            entity.Property(d => d.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.Property(m => m.MetricType)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(m => m.Value)
                  .HasColumnType("float");

            entity.Property(m => m.Unit)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(m => m.Timestamp)
                  .HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(m => m.Device)
                  .WithMany(d => d.Measurements)
                  .HasForeignKey(m => m.DeviceId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Severity)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(a => a.Message)
                  .IsRequired()
                  .HasMaxLength(2000);

            entity.Property(a => a.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(a => a.Device)
                  .WithMany(d => d.Alerts)
                  .HasForeignKey(a => a.DeviceId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
