using DataAccess.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    //Primary CTOR
    internal class DepartmentRepository(ApplicationDbContext dbContext)
    {
        // also can remove
        private readonly ApplicationDbContext _dbContext = dbContext;
        // CRUD Operations
        // Get All
        // Gey By Id
        public Department? GetById(int id)
        {
            var department = _dbContext.Departments.Find(id);

            return department;

        }
        // Insert
        // Update
        // Delete

        // Notes
        #region Traditional_CTOR
        //public DepartmentRepository(ApplicationDbContext dbContext)   // DI
        //{
        //    this._dbContext = dbContext;
        //}
        #endregion
        #region First_Scenario_To_Make_Function
        //public Department? GetById(int id, ApplicationDbContext dbContext)
        //{
        //    var department = dbContext.Departments.Find(id);

        //    return department;

        //} 
        #endregion
        #region Second_Scenario_To_Make_Function
        // bad practise to make object inside other project and you are voilate 2 Principle from SOLID
        //1- dependency inversion principle
        //2- single responsibility principle
        //ApplicationDbContext dbContext = new ApplicationDbContext();
        //public Department? GetById(int id, ApplicationDbContext dbContext)
        //{
        //    var department = dbContext.Departments.Find(id);

        //    return department;
        //}
        #endregion





    }
}
