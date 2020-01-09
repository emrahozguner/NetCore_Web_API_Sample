using API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.Contexts.EntitiesConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd()
                .HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

            ////Fake Data için
            //builder.HasData
            //(
            //    new Category {Id = 100, Name = "Fruits and Vegetables"}, // Id set manually due to in-memory provider
            //    new Category {Id = 101, Name = "Dairy"}
            //);
        }
    }
}