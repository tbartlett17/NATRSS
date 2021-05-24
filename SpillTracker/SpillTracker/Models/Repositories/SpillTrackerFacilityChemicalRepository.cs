using Microsoft.EntityFrameworkCore;
using SpillTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Repositories
{
    public class SpillTrackerFacilityChemicalRepository: Repository<FacilityChemical>, ISpillTrackerFacilityChemicalRepository
    {
        public SpillTrackerFacilityChemicalRepository(SpillTrackerDbContext ctx) : base(ctx)
        {

        }

        public IQueryable<FacilityChemical> GetAllFacilityChemicalsByFacilityId(int id)
        {
            return _dbSet.Include(fc => fc.Chemical)
                .Include(fc => fc.ChemicalState)
                .Where(f => f.FacilityId == id);
        }


    }
}
