using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ranking.Domain.Models;
using System.Linq.Expressions;

namespace Ranking.Domain.Abstract
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        T Get(int id);

        void Delete(Expression<Func<T, bool>> query);
        void Add(T entry);
        void Update(T entry);
        void Delete(T entry);
        void Commit();
        void Dispose();
    }
}
