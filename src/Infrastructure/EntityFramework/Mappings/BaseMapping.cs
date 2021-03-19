using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Mappings
{
    public abstract class BaseMapping<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(entity => entity.Id);
            ConfigureMapping(builder);
        }

        protected abstract void ConfigureMapping(EntityTypeBuilder<T> builder);
    }
}