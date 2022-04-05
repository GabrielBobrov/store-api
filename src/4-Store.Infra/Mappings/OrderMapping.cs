using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Core.Enums;

namespace Store.Infra.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("BIGINT");

            builder.HasOne<Costumer>(c => c.Costumer)
               .WithMany(o => o.Orders)
               .HasForeignKey(f => f.CostumerId);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status");
        }
    }
}