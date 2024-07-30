using Domain.Entities.Product;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.ProductConfig;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.ToTable("Product", "pro");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.ProduceDate)
            .IsRequired();

        builder.Property(p => p.ManufacturePhone)
            .HasMaxLength(15);

        builder.Property(p => p.ManufactureEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.IsAvailable)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade); // Optional: define delete behavior

        // Configure the unique constraint for ManufactureEmail and ProduceDate
        builder.HasIndex(p => new { p.ManufactureEmail, p.ProduceDate })
            .IsUnique();

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}