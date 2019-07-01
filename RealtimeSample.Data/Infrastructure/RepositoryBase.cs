using RealtimeSample.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RealtimeSample.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private RealtimeSampleContext _dbContext;
        private readonly IDbSet<T> _dbSet;

        protected IDbFactory _dbFactory
        {
            get;
            private set;
        }

        protected RealtimeSampleContext DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }
        #endregion

        #region ctro
        protected RepositoryBase(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }


        #endregion

        #region Implementation
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int Id)
        {
            return _dbSet.Find(Id);
        }

        public virtual IEnumerable<T> GetAllAfterTime(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public virtual IEnumerable<T> GetAllDeleted(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }
        #endregion
    }
}
