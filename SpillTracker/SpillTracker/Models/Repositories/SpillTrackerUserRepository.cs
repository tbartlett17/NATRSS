using SpillTracker.Models;
using SpillTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Repositories
{
    public class SpillTrackerUserRepository : Repository<Stuser>, ISpillTrackerUserRepository
    {
        public SpillTrackerUserRepository(SpillTrackerDbContext ctx) : base(ctx)
        {

        }

        public virtual bool Exists(Stuser stu)
        {
            return _dbSet.Any(x => x.AspnetIdentityId == stu.AspnetIdentityId && x.FirstName == stu.FirstName && x.LastName == stu.LastName);
        }

        public virtual Stuser GetStuserByIdentityId(string identityID)
        {
            return _dbSet.Where(u => u.AspnetIdentityId == identityID).FirstOrDefault();
        }

    }
}
