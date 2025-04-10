using DataAccess.Data.Contexts;
using DataAccess.Models.DepartmentModel;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    //Primary CTOR
    public class DepartmentRepository(ApplicationDbContext dbContext) :  GenericRepository<Department>(dbContext) , IDepartmentRepository
    {
        // also can remove
        private readonly ApplicationDbContext _dbContext = dbContext;
        // CRUD Operations
        #region CRUD

        #endregion
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
