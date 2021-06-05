using Microsoft.EntityFrameworkCore;
using SpillTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Repositories
{
    public class SpillTrackerStuserFacilityRepository: Repository<StuserFacility>, ISpillTrackerStuserFacilityRepository
    {
        public SpillTrackerStuserFacilityRepository(SpillTrackerDbContext ctx) : base(ctx)
        {

        }

        public virtual IQueryable<StuserFacility> GetAllAccessibleFacilitiesByUserId(int id)
        {
            return _dbSet.Where(uf => uf.StuserId == id);
        }

        public virtual IQueryable<StuserFacility> GetAllUsersByFacilityId(int id)
        {
            return _dbSet.Include(uf => uf.Facility)
                .Include(uf => uf.Stuser)
                .Where(u => u.FacilityId == id);
        }

        public virtual Task<StuserFacility> GetStuserFacilityByStuserIdAndFacilityIdAsync(int stuserId, int facilityId)
        {
            return _dbSet.Where(uf => uf.StuserId == stuserId && uf.FacilityId == facilityId).FirstOrDefaultAsync();
        }
    }
}
