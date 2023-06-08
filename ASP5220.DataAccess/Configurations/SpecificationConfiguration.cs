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
    public class SpecificationConfiguration : BaseEntityConfiguration<Specification>
    {
        public override void MyConfigurations(EntityTypeBuilder<Specification> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
