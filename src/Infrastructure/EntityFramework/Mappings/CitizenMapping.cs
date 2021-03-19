using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Mappings
{
    public class CitizenMapping : BaseMapping<Citizen>
    {
        protected override void ConfigureMapping(EntityTypeBuilder<Citizen> builder)
        {
            builder
                .Property<string>("_name")
                .HasColumnName("Name")
                .IsRequired();
        }
    }
}