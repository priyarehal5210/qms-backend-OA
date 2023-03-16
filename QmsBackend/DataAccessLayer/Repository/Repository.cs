using DataAccessLayer.Context;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity :class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();

        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();

        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string IncludeProperties = null)
        {
         IQueryable<TEntity>query=_context.Set<TEntity>();
            if(filter!=null)
                query = query.Where(filter);
            if (IncludeProperties != null)
            {
                foreach (var includeprop in IncludeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            if(orderby!=null)
                return await orderby(query).ToListAsync();
            return await query.ToListAsync();
        }

        public TEntity GetByIdAsync(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public async Task RemoveAsync(TEntity entity)
        {
           _context.Set<TEntity>().Remove(entity);
           await _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
