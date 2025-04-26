using DataAccess.Data.Contexts;
using DataAccess.Models.DepartmentModel;
using DataAccess.Models.Shared;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        // Get All
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return _dbContext.Set<TEntity>().Where(E=>E.IsDeleted !=true).ToList();
            }
            else  
            {
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
            }
        }
        // Gey By Id 
        public TEntity? GetById(int id) 
        {
            var TEntity = _dbContext.Set<TEntity>().Find(id);

            return TEntity; 

        }
        // Insert
        public void Add(TEntity TEntity)
        {
            _dbContext.Set<TEntity>().Add(TEntity); //localy
        }
        // Update
        public void Update(TEntity TEntity)
        {
            _dbContext.Set<TEntity>().Update(TEntity); //localy

        }
        // Delete
        public void Remove(TEntity TEntity)
        {
            _dbContext.Set<TEntity>().Remove(TEntity); //localy

        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return _dbContext.Set<TEntity>().Where(Predicate).ToList();
        }
    }
}
