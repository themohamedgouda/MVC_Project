using DataAccess.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        int Add(TEntity Entity);
        IEnumerable<TEntity> GetAll( bool WithTracking = false);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> Predicate);
        TEntity? GetById(int id);
        int Remove(TEntity Entity);
        int Update(TEntity Entity);

    }
}
