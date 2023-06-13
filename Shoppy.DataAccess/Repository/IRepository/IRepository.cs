using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoppy.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // Common Methods to Consider
        // GET ALL, GET by ID First or Default, ADD, REMOVE, REMOVERANGE...etc,

        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        IEnumerable<T> GetAll();
        T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null);
    }
}
