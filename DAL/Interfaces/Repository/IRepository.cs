using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Repository
{
   public interface IRepository<T>
        where T:class
    {
        Task<T> Get(int? id);
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
        Task<IEnumerable<T>> GetList();
        Task<IEnumerable<T>> Query(Expression<Func<T, bool>> filter);
        Task<T> Get(Expression<Func<T, bool>> filter);
    }
}
