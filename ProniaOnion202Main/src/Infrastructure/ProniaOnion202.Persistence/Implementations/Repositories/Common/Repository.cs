using Microsoft.EntityFrameworkCore;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Abstractions.Repository;
using ProniaOnion202.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProniaOnion202.Persistence.Implementations.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        public IQueryable<T> GetAll(bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table; 
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if(!isTracking) query=query.AsNoTracking();
            query = _addIncludes(query, includes);
            return query;
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool isDescending = false, int skip = 0, int take = 0, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (expression != null) query = query.Where(expression);
            if (orderExpression != null)
            {
                if (isDescending) query = query.OrderByDescending(orderExpression);
                else query = query.OrderBy(orderExpression);
            }
            if (skip != 0) query = query.Skip(skip);
            if (take != 0) query = query.Take(take);
            query = _addIncludes(query, includes);
            if (ignoreQuery) query = query.IgnoreQueryFilters();

            return isTracking ? query : query.AsNoTracking();
        }
        public async Task<T> GetByIdAsync(int id, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {

            IQueryable<T> query = _table.Where(d=>d.Id==id);
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();

           
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>>? expression = null, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(expression);
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracking) query = query.AsNoTracking();
            query=_addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }
       
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }
        public void Delete(T entity)
        {
            _table.Remove(entity);
        }
        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
        }
        public void ReverseDelete(T entity)
        {
            entity.IsDeleted=false;
        }   
       
        public async Task SaveChangesAsync()
        {
            _context.SaveChangesAsync();
        }

        private  IQueryable<T> _addIncludes(IQueryable<T> query,params string[] includes)
        {
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;   
        }
    }
}
