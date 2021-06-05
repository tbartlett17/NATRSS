using Microsoft.EntityFrameworkCore;
using SpillTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Repositories
{
    public class SpillTrackerFacilityRepository: Repository<Facility>, ISpillTrackerFacilityRepository
    {
        public SpillTrackerFacilityRepository(SpillTrackerDbContext ctx) : base(ctx)
        {

        }

        public virtual bool FacilityExists(int id)
        {
            return _dbSet.Any(f => f.Id == id);
        }

        public override IQueryable<Facility> GetAll()
        {
            return _dbSet.Include(f => f.Company);
        }

        public virtual IQueryable<Facility> GetAllFacilitiesByIdList(List<int?> usersAccessibleFacilitiesIds)
        {
            return _dbSet.Include(f => f.Company)
                .Where(f => usersAccessibleFacilitiesIds
                .Contains(f.Id));
        }

        public virtual Facility GetFacilityByAccessCode(string code)
        {
            return _dbSet.Where(f => f.AccessCode.ToUpper().Equals(code))
                .FirstOrDefault();
        }
        public virtual bool FacilityAccessCodeExists(string code)
        {
            return _dbSet.Any(f => f.AccessCode.ToUpper().Equals(code));
        }

        public virtual int GetFacilityIdByCompanyIdAndFacilityAccessCode(int companyId, string accessCode)
        {
            return _dbSet.Where(f => f.CompanyId == companyId && f.AccessCode == accessCode).FirstOrDefault().Id;
        }



    }
}
