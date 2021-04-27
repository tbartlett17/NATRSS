using SpillTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Interfaces
{
    public interface ISpillTrackerFormRepository : IRepository<Form>
    {
        IEnumerable<Form> GetAllFormsByCompanyId(int id);
        IEnumerable<Form> GetAllForms();
        bool FormExists(int id);
    }
}
