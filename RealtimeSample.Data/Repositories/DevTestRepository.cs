using RealtimeSample.Data.Infrastructure;
using RealtimeSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeSample.Data.Repositories
{
    public class DevTestRepository : RepositoryBase<DevTest>, IDevTestRepository
    {
        public DevTestRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
    public interface IDevTestRepository : IRepository<DevTest>
    {
    }
}
