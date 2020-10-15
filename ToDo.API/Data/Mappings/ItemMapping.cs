using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.API.Models;

namespace ToDo.API.Data.Mappings
{
    public class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.CreatedDate).IsRequired()
                .HasColumnType("datetime");
            builder.Property(i => i.DueDate).IsRequired()
                .HasColumnType("datetime");
            builder.Property(i => i.UpdatedDate)
                .HasColumnType("datetime");
            builder.Property(i => i.FinishedDate)
                .HasColumnType("datetime");
            builder.Property(i => i.UserId).IsRequired()
                .HasColumnType("integer");
            builder.Property(i => i.Description).IsRequired()
                .HasColumnType("varchar(500)");

            builder.HasOne(i => i.User).WithMany(u => u.Items).HasForeignKey(i => i.UserId);
        }
    }

}
