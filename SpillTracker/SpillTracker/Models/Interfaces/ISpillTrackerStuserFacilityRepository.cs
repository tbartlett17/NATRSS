using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Interfaces
{
    public interface ISpillTrackerStuserFacilityRepository: IRepository<StuserFacility>
    {
        IQueryable<StuserFacility> GetAllAccessibleFacilitiesByUserId(int id);
        IQueryable<StuserFacility> GetAllUsersByFacilityId(int id);
        Task<StuserFacility> GetStuserFacilityByStuserIdAndFacilityIdAsync(int stuserId, int facilityId);
    }
}
