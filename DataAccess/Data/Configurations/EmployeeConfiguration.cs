using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Configurations
{
    public class EmployeeConfiguration :BaseEntityConfigurations<Employee> , IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Address).HasColumnType("varchar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E => E.Gender).HasConversion((EmpGender) => EmpGender.ToString(),
               (_Gender) => (Gender)Enum.Parse(typeof(Gender), _Gender));
            builder.Property(E => E.EmployeeType).HasConversion((EmployeeType) => EmployeeType.ToString(),
               (_EmployeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), _EmployeeType));
            base.Configure(builder);
        }
    }
}
