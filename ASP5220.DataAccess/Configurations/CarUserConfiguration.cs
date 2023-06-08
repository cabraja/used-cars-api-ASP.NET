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
    public class CarUserConfiguration : IEntityTypeConfiguration<CarUser>
    {
        public void Configure(EntityTypeBuilder<CarUser> builder)
        {
            builder.HasKey(x => new { x.CarId,x.UserId});
            builder.HasOne(x => x.User).WithMany(x => x.FollowedCars).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Car).WithMany(x => x.Followers).HasForeignKey(x => x.CarId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
