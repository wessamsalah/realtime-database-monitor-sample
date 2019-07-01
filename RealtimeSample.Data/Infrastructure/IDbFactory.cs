using RealtimeSample.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeSample.Data.Infrastructure
{
    public interface IDbFactory :IDisposable
    {
        RealtimeSampleContext Init();
    }
}
