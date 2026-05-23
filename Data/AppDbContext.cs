using Apbd5.Models;
using Microsoft.EntityFrameworkCore;

namespace Apbd5.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PC> PCs => Set<PC>();
    public DbSet<Component> Components => Set<Component>();
    public DbSet<PCComponent> PCComponents => Set<PCComponent>();
    public DbSet<ComponentType> ComponentTypes => Set<ComponentType>();
    public DbSet<ComponentManufacturer> ComponentManufacturers => Set<ComponentManufacturer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PC>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Weight).IsRequired();
            entity.Property(e => e.Warranty).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Stock).IsRequired();
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Abbreviation).HasMaxLength(30).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(150).IsRequired();
        });

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Abbreviation).HasMaxLength(30).IsRequired();
            entity.Property(e => e.FullName).HasMaxLength(300).IsRequired();
            entity.Property(e => e.FoundationDate).IsRequired();
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Code);
            entity.Property(e => e.Code).HasMaxLength(10).IsFixedLength().IsRequired();
            entity.Property(e => e.Name).HasMaxLength(300).IsRequired();
            entity.Property(e => e.Description).IsRequired();

            entity.HasOne(e => e.Manufacturer)
                .WithMany(m => m.Components)
                .HasForeignKey(e => e.ComponentManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Type)
                .WithMany(t => t.Components)
                .HasForeignKey(e => e.ComponentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.HasKey(e => new { e.PCId, e.ComponentCode });

            entity.Property(e => e.ComponentCode).HasMaxLength(10).IsFixedLength().IsRequired();

            entity.HasOne(e => e.PC)
                .WithMany(pc => pc.PCComponents)
                .HasForeignKey(e => e.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(e => e.ComponentCode)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Central Processing Unit" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Processing Unit" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Random Access Memory" }
        );

        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1, Abbreviation = "INT", FullName = "Intel Corporation", FoundationDate = new DateOnly(1968, 7, 18)
            },
            new ComponentManufacturer
            {
                Id = 2, Abbreviation = "AMD", FullName = "Advanced Micro Devices",
                FoundationDate = new DateOnly(1969, 5, 1)
            },
            new ComponentManufacturer
            {
                Id = 3, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateOnly(1993, 4, 5)
            }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "COMP-00001", Name = "Intel Core i9-14900K", Description = "grzalka",
                ComponentManufacturerId = 1, ComponentTypeId = 1
            },
            new Component
            {
                Code = "COMP-00002", Name = "AMD Ryzen 9 7950X", Description = "cpu",
                ComponentManufacturerId = 2, ComponentTypeId = 1
            },
            new Component
            {
                Code = "COMP-00003", Name = "NVIDIA RTX 4090", Description = "czemu to kosztuje 15k..",
                ComponentManufacturerId = 3, ComponentTypeId = 2
            }
        );

        modelBuilder.Entity<PC>().HasData(
            new PC
            {
                Id = 1, Name = "pc z biedry", Weight = 12.5f, Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5
            },
            new PC
            {
                Id = 2, Name = "super desktop", Weight = 4.2f, Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12
            },
            new PC
            {
                Id = 3, Name = "mac studio", Weight = 15.0f, Warranty = 24,
                CreatedAt = new DateTime(2026, 1, 10, 10, 0, 0), Stock = 3
            }
        );

        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent { PCId = 1, ComponentCode = "COMP-00001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "COMP-00003", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "COMP-00001", Amount = 1 }
        );
    }
}