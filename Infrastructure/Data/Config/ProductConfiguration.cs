using API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(o => o.Id).IsRequired();
            builder.Property(o => o.NameAr).IsRequired().HasMaxLength(100);
            builder.Property(o => o.NameEn).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Price).HasPrecision(18, 2);
            builder.Property(o => o.ImgUrl).IsRequired();
            
            builder.HasOne(o => o.ProductBrand).WithMany()
                .HasForeignKey(o => o.ProductBrandId);

            builder.HasOne(o => o.ProductType).WithMany()
                .HasForeignKey(o => o.ProductTypeId);
        }
    }
}