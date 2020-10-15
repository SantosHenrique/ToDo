using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.API.Models;

namespace ToDo.API.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).IsRequired()
                .HasColumnType("varchar(150)");
            builder.Property(u => u.Password).IsRequired()
                .HasColumnType("varchar(30)");
            builder.Property(u => u.Role).IsRequired()
                .HasColumnType("varchar(30)");

            builder.HasMany(u => u.Items).WithOne(t => t.User);
        }
    }
}
