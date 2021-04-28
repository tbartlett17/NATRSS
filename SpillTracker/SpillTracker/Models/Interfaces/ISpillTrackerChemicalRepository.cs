using SpillTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models.Interfaces
{
    public interface ISpillTrackerChemicalRepository : IRepository<Chemical>
    {
        bool ChemExists(int id);

        Chemical GetChemByCAS(string casNumber);

        List<Chemical> ByFirstLetter(string l);
    }
}