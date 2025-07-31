using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shop.Domain
{
    public interface IBaseRepository<Tkey, T> where T : class
    {
        void Create(T entity);
        T Get(Tkey id);
        List<T> GetAll();
        bool Exists(Expression<Func<T, bool>> expression);

        void SaveChanges();
    }
}