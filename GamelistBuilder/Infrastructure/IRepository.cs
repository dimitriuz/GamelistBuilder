using GamelistBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Infrastructure
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        //IEnumerable<ESSystem> Find(Func<ESSystem, bool> predicate);
        //int Count(Func<ESSystem, bool> predicate);

        T GetById(string id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
