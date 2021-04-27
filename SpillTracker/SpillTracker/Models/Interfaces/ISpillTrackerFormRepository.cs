using SpillTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Interfaces
{
    interface ISpillTrackerFormRepository : IRepository<Form>
    {
        IEnumerable<Form> GetAllFormsByCompanyId(int id);
    }
}
