using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Interfaces
{
    public interface ISpillTrackerFacilityChemicalRepository: IRepository<FacilityChemical>
    {
        IQueryable<FacilityChemical> GetAllFacilityChemicalsByFacilityId(int id);
    }
}
