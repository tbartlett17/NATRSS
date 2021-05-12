using SpillTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Repositories
{
    public class SpillTrackerCompanyRepository: Repository<Company>, ISpillTrackerCompanyRepository
    {
        public SpillTrackerCompanyRepository(SpillTrackerDbContext ctx) : base(ctx)
        {

        }

        public virtual Company GetCompanyByAccessCode(string code)
        {
            return _dbSet.Where(f => f.AccessCode.ToUpper().Equals(code))
                .FirstOrDefault();
        }
    }
}
