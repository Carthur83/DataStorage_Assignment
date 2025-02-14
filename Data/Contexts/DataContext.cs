using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StatusEntity>()
            .HasIndex(x => x.StatusType)
            .IsUnique();

        //följande kod är från chatgpt
        modelBuilder.Entity<StatusEntity>().HasData(
            new StatusEntity { Id = 1, StatusType = "Ej Påbörjad" },
            new StatusEntity { Id = 2, StatusType = "Pågående" },
            new StatusEntity { Id = 3, StatusType = "Slutförd" }
         );

        modelBuilder.Entity<ProjectEntity>()
       .Property(e => e.Id)
       .UseIdentityColumn(101, 1);

        modelBuilder.Entity<EmployeeEntity>()
       .Property(e => e.Id)
       .UseIdentityColumn(1001, 1);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(x => x.Service)
            .WithMany()
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
