using RealtimeSample.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeSample.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private RealtimeSampleContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public RealtimeSampleContext DbContext {
           get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }

        public void commit()
        {
            DbContext.Commit();
        }
    }
}
