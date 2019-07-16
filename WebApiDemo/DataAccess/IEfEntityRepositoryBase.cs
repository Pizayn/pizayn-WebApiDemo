using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApiDemo.Entities;

namespace WebApiDemo.DataAccess
{
    public interface IEfEntityRepositoryBase<TEntity>
    where TEntity:class,IEntity,new()
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filterExpression = null);
        TEntity Get(Expression<Func<TEntity, bool>> filterExpression = null);
    }
}
