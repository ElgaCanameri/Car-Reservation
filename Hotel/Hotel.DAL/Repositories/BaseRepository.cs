using Hotel.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    public interface IBaseRepository<T, T1>
    {
        T GetById(int id);
        void Delete(T1 id);
        T1 Add(T entity);
        IEnumerable<T> GetAll();
        T Update(T entity);

    }
    public abstract class BaseRepository<T, T1> where T : BaseEntity<T1>
    {
        protected readonly DbSet<T> _set;
        public BaseRepository(AppDbContext dbContext)
        {
            _set = dbContext.Set<T>();
        }

        public T GetById(T1 id)
        {
            return _set.Find(id);
        }
        public void Delete(T1 id)
        {
            var entity = GetById(id);
            _set.Remove(entity);
        }
        public T1 Add(T entity)
        {
            _set.Add(entity);
            return entity.Id;
        }

        public IEnumerable<T> GetAll()
        {
            return _set.ToList();
        }
        public T Update(T entity)
        {
            _set.Update(entity);
            return entity;
        }

    }
}