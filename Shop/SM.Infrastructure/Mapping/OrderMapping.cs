using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sm.Domain.OrderAgg;

namespace SM.Infrastructure.Mapping
{
    public class OrderMapping: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IssueTrackingNum).HasMaxLength(8);
            builder.OwnsMany(x => x.Items, NavigationBuilder =>
            {
                NavigationBuilder.ToTable("OrderItems");
                NavigationBuilder.HasKey(x => x.Id);
                NavigationBuilder.WithOwner(x => x.Order)
                    .HasForeignKey(x => x.OrderId);
                
                

            });
        }
    }
}