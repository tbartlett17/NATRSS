using SpillTracker.Models;
using SpillTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SpillTracker.Models.Repositories
{
    public class SpillTrackerFormRepository : Repository<Form>, ISpillTrackerFormRepository
    {
        public SpillTrackerFormRepository(SpillTrackerDbContext ctx) : base(ctx)
        {

        }

        public virtual IQueryable<Form> GetAllFormsByCompanyId(int id)
        {
            return _dbSet.Include(f => f.FacilityChemical)
                .ThenInclude(fc => fc.Chemical)
                .Include(f => f.FacilityChemical)
                .ThenInclude(fc => fc.ChemicalState)
                .Include(f => f.ChemicalState)
                .Include(f => f.Facility)
                .Include(f => f.SpillSurface)
                .Include(f => f.Stuser)
                .Where(c => c.Facility.CompanyId == id);
        }

        public override IQueryable<Form> GetAll()
        {
            return _dbSet.Include(f => f.FacilityChemical)
                .ThenInclude(fc => fc.Chemical)
                .Include(f => f.FacilityChemical)
                .ThenInclude(fc => fc.ChemicalState)
                .Include(f => f.ChemicalState)
                .Include(f => f.Facility)
                .Include(f => f.SpillSurface)
                .Include(f => f.Stuser);
        }

        public virtual bool FormExists(int id)
        {
            return _dbSet.Any(f => f.Id == id);
        }
    }
}
