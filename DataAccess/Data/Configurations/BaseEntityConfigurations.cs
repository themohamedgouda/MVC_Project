using DataAccess.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Configurations
{
    public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(D => D.CreateOn).HasDefaultValueSql("getdate()"); //  one time
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("getdate()"); // recaluc every change
        }
    }
}
