using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Interfaces
{
    public interface ISpillTrackerFacilityRepository : IRepository<Facility>
    {
        bool FacilityExists(int id);
        new IQueryable<Facility> GetAll();
        IQueryable<Facility> GetAllFacilitiesByIdList(List<int?> usersAccessibleFacilitiesIds);
        Facility GetFacilityByAccessCode(string code);
        bool FacilityAccessCodeExists(string code);
        int GetFacilityIdByCompanyIdAndFacilityAccessCode(int companyId, string accessCode);
    }
}
