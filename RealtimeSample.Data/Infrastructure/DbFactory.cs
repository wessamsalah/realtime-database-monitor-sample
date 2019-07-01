using RealtimeSample.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealtimeSample.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        RealtimeSampleContext dbContext;

        public RealtimeSampleContext Init()
        {
            return dbContext ?? (dbContext = new RealtimeSampleContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}