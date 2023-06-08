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
    public class CarConfiguration : BaseEntityConfiguration<Car>
    {
        public override void MyConfigurations(EntityTypeBuilder<Car> builder)
        {
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Model).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Variant).HasMaxLength(50);
            builder.Property(x => x.Mileage).IsRequired();
            builder.Property(x => x.Power).IsRequired();
            builder.Property(x => x.EngineCapacity).IsRequired();

            builder.HasMany(x => x.Followers).WithOne(x => x.Car).HasForeignKey(x => x.CarId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Files).WithOne(x => x.Car).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Specifications).WithOne(x => x.Car).HasForeignKey(x => x.CarId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
