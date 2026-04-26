using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace GoodHamburger.Infrastructure.Persistence;

public sealed class GoodHamburgerDbContext : DbContext
{
    public GoodHamburgerDbContext(DbContextOptions<GoodHamburgerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasKey(order => order.Id);

            builder.Property(order => order.Subtotal)
                .HasPrecision(10, 2);

            builder.Property(order => order.DiscountPercentage)
                .HasPrecision(5, 2);

            builder.Property(order => order.DiscountAmount)
                .HasPrecision(10, 2);

            builder.Property(order => order.Total)
                .HasPrecision(10, 2);

            builder.OwnsMany(order => order.Items, itemBuilder =>
            {
                itemBuilder.WithOwner()
                    .HasForeignKey("OrderId");

                itemBuilder.HasKey(item => item.Id);

                itemBuilder.Property(item => item.Id)
                    .ValueGeneratedNever();

                itemBuilder.Property(item => item.Code)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                itemBuilder.Property(item => item.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                itemBuilder.Property(item => item.Type)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                itemBuilder.Property(item => item.UnitPrice)
                    .HasPrecision(10, 2);
            });

            builder.Navigation(order => order.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}