﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiDemo.Entities;

namespace WebApiDemo.DataAccess
{
    public class EfEntityRepositoryBase<TContext,TEntity>:IEfEntityRepositoryBase<TEntity>
    where TEntity:class,IEntity,new()
    where TContext:DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }

        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            using (TContext context = new TContext())
            {
                return filterExpression == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filterExpression).ToList();
            }

        }

        public TEntity Get(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filterExpression);
            }
        }
    }
}
