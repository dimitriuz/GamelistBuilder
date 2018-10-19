using GamelistBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        int Count(Func<T, bool> predicate);

        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
