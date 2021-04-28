using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpillTracker.Models;


namespace SpillTracker.Models.Interfaces
{
    public interface ISpillTrackerUserRepository : IRepository<Stuser>
    {
        Stuser GetStuserByIdentityId(string identityId);
        bool Exists(Stuser stu);

        
    }
}
