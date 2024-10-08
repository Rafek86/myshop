using Microsoft.EntityFrameworkCore;
using myshop.DataAccess.Data;
using myshop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(ApplicationDbContext context) { 
        _context = context;
         _dbset = _context.Set<T>();    
        }

        public void Add(T entity)
        {
           _dbset.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, string? IncludeWord)
        {
            IQueryable<T> query = _dbset;
            if (predicate != null) { 
            query = query.Where(predicate);
            }
            if (IncludeWord != null) {
                foreach (var item in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)){
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate, string? IncludeWord)
        {
            IQueryable<T> query = _dbset;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (IncludeWord != null)
            {
                foreach (var item in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
         _dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
        _dbset.RemoveRange(entities);
        }
    }
}
