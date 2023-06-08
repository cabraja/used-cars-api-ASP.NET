using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP5220.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP5220.DataAccess.Configurations
{
    public class SpecificationCarConfiguration : IEntityTypeConfiguration<SpecificationCar>
    {
        public void Configure(EntityTypeBuilder<SpecificationCar> builder)
        {
            builder.HasKey(x => new { x.CarId, x.SpecificationId, x.SpecificationValueId });
            //builder.HasOne(x => x.Car).WithMany(x => x.Specifications).HasForeignKey(x => x.CarId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Specification).WithMany(x => x.SpecificationCars).HasForeignKey(x => x.SpecificationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SpecificationValue).WithMany(x => x.SpecificationCars).HasForeignKey(x => x.SpecificationValueId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
