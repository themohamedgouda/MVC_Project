using DataAccess.Data.Contexts;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class UnitOfWork(IDepartmentRepository _departmentRepository, IEmployeeRepository _employeeRepository, ApplicationDbContext _dbContext) : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository => _employeeRepository;
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        //public void Dispose()
        //{
        //    _dbContext.Dispose();
        //}
        // already here
        public int SaveChanges() => _dbContext.SaveChanges();
        
    }
}
