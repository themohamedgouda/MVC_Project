using DataAccess.Data.Contexts;
using DataAccess.Models.DepartmentModel;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext _dbContext)  : GenericRepository<Employee>(_dbContext) , IEmployeeRepository
    {

    }
}
