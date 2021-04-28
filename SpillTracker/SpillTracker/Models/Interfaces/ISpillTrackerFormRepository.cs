using SpillTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Interfaces
{
    public interface ISpillTrackerFormRepository : IRepository<Form>
    {
        IQueryable<Form> GetAllFormsByCompanyId(int id);
        new IQueryable<Form> GetAll();
        bool FormExists(int id);
    }
}
