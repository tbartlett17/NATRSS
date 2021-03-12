namespace SpillTracker.Models
{

  
    public class ExtraChemData
    {
        public int CID { get; set; }
        public double MolecularWeight { get; set; }
        public string MoleculWeightUnits { get; set; }
        public string MolecularWeightReference { get; set; }
        public double Density { get; set; }
        public string DensityUnits { get; set; }
        public string DensityReference { get; set; }
        public double VaporPressure { get; set; }
        public string VaporPressureUnits { get; set; }
        public string VaporPressureReference { get; set; }
        public string Message { get; set; }



    }

}