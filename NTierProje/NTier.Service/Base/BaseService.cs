using NTier.Core.Entity;
using NTier.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NTier.Model.Context;
using System.Data.Entity.Infrastructure;

namespace NTier.Service.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        //Singleton context için bu şekilde tanımlıyoruz....

        private static ProjectContext _db;

        public ProjectContext Db
        {
            get {
                if (_db == null)
                {
                    _db = new ProjectContext();
                    return _db;
                }
                return _db;
            }
            set {
                _db = value;
            }
        }

        public void Add(List<T> items)
        {
            Db.Set<T>().AddRange(items);
            Save();
        }

        public void Add(T item)
        {
            Db.Set<T>().Add(item);
            Save();
        }

        public bool Any(Expression<Func<T, bool>> exp) =>
            Db.Set<T>().Any(exp);
            

        public List<T> GetActive()
        {
            return Db.Set<T>().Where(x => x.Status == Core.Entity.Enum.Status.Active).ToList();
        }

        public List<T> GetAll()
        {
            return Db.Set<T>().ToList();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return Db.Set<T>().Where(exp).FirstOrDefault();
        }

        public T GetById(Guid id)
        {
            return Db.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return Db.Set<T>().Where(exp).ToList();
        }

        public void Remove(Guid id)
        {
            T item = GetById(id);
            item.Status = Core.Entity.Enum.Status.Deleted;
            Update(item);
        }

        public void Remove(T item)
        {
            item.Status = Core.Entity.Enum.Status.Deleted;
            Update(item);
        }

        public void RemoveAll(Expression<Func<T, bool>> exp)
        {
            foreach (var item in GetDefault(exp))
            {
                item.Status = Core.Entity.Enum.Status.Deleted;
                Update(item);
            }
        }

        public int Save()
        {
            return Db.SaveChanges();
        }

        public void Update(T item)
        {
            T updated = GetById(item.Id);
            DbEntityEntry entry = Db.Entry(updated);
            entry.CurrentValues.SetValues(item);
            Save();
        }

        //Singleton pattern tarafı ile ilgili cache sorunun engellemek için DetachEntity metodunu yazmalıyız.
        public void DetachEntity(T item)
        {
            Db.Entry<T>(item).State = System.Data.Entity.EntityState.Detached;
        }
    }
}
