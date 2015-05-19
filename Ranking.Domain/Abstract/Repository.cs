using Ranking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Ranking.Domain.Abstract
{
    public class Repository<T> : IRepository<T>, IDisposable
        where T : class
    {
        private PlacesDbContext _context;
        private IDbSet<T> _dbSet;
        private bool _disposed = false;

        public Repository()
        {
            _context = new PlacesDbContext();
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entry)
        {
            _dbSet.Add(entry);
        }

        public void Update(T entry)
        {
            _context.Entry(entry).State = EntityState.Modified;
        }

        public void Delete(T entry)
        {
            _dbSet.Attach(entry);
            _dbSet.Remove(entry);
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> query)
        {
            foreach (var item in _dbSet.Where(query))
                Delete(item);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _context.Dispose();

            _disposed = true;
        }
    }
}
