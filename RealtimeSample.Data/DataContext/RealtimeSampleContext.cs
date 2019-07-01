using RealtimeSample.Model;
using System.Data.Entity;

namespace RealtimeSample.Data.DataContext
{
    public class RealtimeSampleContext : DbContext
    {
        public RealtimeSampleContext():base("ClickTrackerEntities")
        {

        }    
        public DbSet<DevTest> DevTests { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
