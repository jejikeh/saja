using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using saja.Interfaces;

namespace saja;

public class ModelUserBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IUserModel
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(user => user.UserId);
        builder.HasIndex(user => user.UserId).IsUnique();
        builder.HasIndex(user => user.Username).IsUnique();
        builder.Property(user => user.PasswordHash).IsRequired();
    }
}