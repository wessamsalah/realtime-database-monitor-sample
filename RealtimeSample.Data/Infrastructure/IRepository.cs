using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeSample.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Add new entity
        void Add(T entity);
        // Update exist entity
        void Update(T entity);
        // Remove Entity
        void Delete(T entity);
        //Retrive all entities
        IEnumerable<T> GetAll();
        //Retrive entity by id
        T GetById(int Id);
        //Get recent added entities
        IEnumerable<T> GetAllAfterTime(Expression<Func<T, bool>> where);
        //Get recent removed entities
        IEnumerable<T> GetAllDeleted(Expression<Func<T, bool>> where);

    }
}
