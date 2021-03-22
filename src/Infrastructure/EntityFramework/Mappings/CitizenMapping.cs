using System;
using Domain;
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

            builder
                .Property<DateTime>("_dateOfBirth")
                .HasColumnName("DateOfBirth")
                .IsRequired();

            builder
                .Property<Gender>("_gender")
                .HasColumnName("Gender")
                .IsRequired();
        }
    }
}