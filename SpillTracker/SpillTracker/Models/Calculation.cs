using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SpillTracker.Models
{
    public class Calculation 
    {

        public string input {get; set;}
        
        public IEnumerable<Chemical> chemicals {get; set;}

        public int chemID {get; set;}

        public string chemName {get; set;}

        public string chemNum {get; set;}

        public bool? cercla {get; set;}

        public double? reportableQuantity {get; set;}

        public IEnumerable<FacilityChemical> facilityChems {get; set;}

    }
}