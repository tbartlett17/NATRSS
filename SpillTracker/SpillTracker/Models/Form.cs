using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SpillTracker.Models
{
    [Table("Form")]
    public partial class Form
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Spill_Reported_By")]
        [StringLength(50)]
        public string SpillReportedBy { get; set; }
        [Column("Spill_Reported_Date", TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy MMM dd}")]
        public DateTime? SpillReportedDate { get; set; }
        [Column("Spill_Reported_Time")]
        public TimeSpan? SpillReportedTime { get; set; }
        [Column("Spill_Location")]
        [StringLength(100)]
        public string SpillLocation { get; set; }
        [Column("Spill_Ongoing")]
        public bool? SpillOngoing { get; set; }
        [Column("Spill_Contained")]
        public bool? SpillContained { get; set; }
        [Column("Need_Assistance")]
        public bool? NeedAssistance { get; set; }
        [Column("Chemical_Pressurized")]
        public bool? ChemicalPressurized { get; set; }
        [Column("Spill_Volume")]
        public double? SpillVolume { get; set; }
        [Column("Spill_Volume_Units")]
        [StringLength(30)]
        public string SpillVolumeUnits { get; set; }
        [Column("Chemical_Concentration")]
        public double? ChemicalConcentration { get; set; }
        [Column("Spill_Forming_Puddle")]
        public bool? SpillFormingPuddle { get; set; }
        [Column("Spill_Reach_Water_Source")]
        public bool? SpillReachWaterSource { get; set; }
        [Column("Water_Source")]
        [StringLength(100)]
        public string WaterSource { get; set; }
        [Column("Spill_Duration_Hours")]
        public double? SpillDurationHours { get; set; }
        [Column("Spill_Duration_Minutes")]
        public double? SpillDurationMinutes { get; set; }
        [Column("Cleanup_StartTime", TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:yyyy MMM dd hh:mm tt}")]
        public DateTime? CleanupStartTime { get; set; }
        [Column("Chemical_Temperature")]
        public double? ChemicalTemperature { get; set; }
        [Column("Chemical_Temperature_Units")]
        [StringLength(30)]
        public string ChemicalTemperatureUnits { get; set; }
        [Column("Spill_Width")]
        public double? SpillWidth { get; set; }
        [Column("Spill_Width_Units")]
        [StringLength(30)]
        public string SpillWidthUnits { get; set; }
        [Column("Spill_Length")]
        public double? SpillLength { get; set; }
        [Column("Spill_Length_Units")]
        [StringLength(30)]
        public string SpillLengthUnits { get; set; }
        [Column("Spill_Depth")]
        public double? SpillDepth { get; set; }
        [Column("Spill_Depth_Units")]
        [StringLength(30)]
        public string SpillDepthUnits { get; set; }
        [Column("Spill_Area")]
        public double? SpillArea { get; set; }
        [Column("Spill_Area_Units")]
        [StringLength(30)]
        public string SpillAreaUnits { get; set; }
        [Column("Spill_Reportable")]
        public bool? SpillReportable { get; set; }
        [Column("Wind_Direction")]
        [StringLength(10)]
        public string WindDirection { get; set; }
        [Column("Wind_Speed")]
        public double? WindSpeed { get; set; }
        [Column("Wind_Speed_Units")]
        [StringLength(15)]
        public string WindSpeedUnits { get; set; }
        [Column("Address_Street")]
        [StringLength(100)]
        public string AddressStreet { get; set; }
        [Column("Address_City")]
        [StringLength(100)]
        public string AddressCity { get; set; }
        [Column("Address_State")]
        [StringLength(50)]
        public string AddressState { get; set; }
        [Column("Address_ZIP")]
        [StringLength(15)]
        public string AddressZip { get; set; }
        [Column("Weather_Temperature")]
        public double? WeatherTemperature { get; set; }
        [Column("Weather_Temperature_Units")]
        [StringLength(30)]
        public string WeatherTemperatureUnits { get; set; }
        [Column("Weather_Humidity")]
        public double? WeatherHumidity { get; set; }
        [Column("Weather_Humidity_Units")]
        [StringLength(15)]
        public string WeatherHumidityUnits { get; set; }
        [Column("Sky_Conditions")]
        [StringLength(50)]
        public string SkyConditions { get; set; }
        [Column("Spill_Evaporation_Rate")]
        public double? SpillEvaporationRate { get; set; }
        [Column("Spill_Evaporation_Rate_Units")]
        [StringLength(10)]
        public string SpillEvaporationRateUnits { get; set; }
        [Column("Amount_Evaporated")]
        public double? AmountEvaporated { get; set; }
        [Column("Amount_Evaporated_Units")]
        [StringLength(10)]
        public string AmountEvaporatedUnits { get; set; }
        [Column("Amount_Spilled")]
        public double? AmountSpilled { get; set; }
        [Column("Amount_Spilled_Units")]
        [StringLength(10)]
        public string AmountSpilledUnits { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        [StringLength(500)]
        public string ContactNotes { get; set; }
        [Column("STUserID")]
        public int? StuserId { get; set; }
        [Column("FacilityChemicalID")]
        public int? FacilityChemicalId { get; set; }
        [Column("Spill_SurfaceID")]
        public int? SpillSurfaceId { get; set; }
        [Column("ChemicalStateID")]
        public int? ChemicalStateId { get; set; }
        [Column("FacilityID")]
        public int? FacilityId { get; set; }

        [ForeignKey(nameof(ChemicalStateId))]
        [InverseProperty("Forms")]
        public virtual ChemicalState ChemicalState { get; set; }
        [ForeignKey(nameof(FacilityId))]
        [InverseProperty("Forms")]
        public virtual Facility Facility { get; set; }
        [ForeignKey(nameof(FacilityChemicalId))]
        [InverseProperty("Forms")]
        public virtual FacilityChemical FacilityChemical { get; set; }
        [ForeignKey(nameof(SpillSurfaceId))]
        [InverseProperty(nameof(Surface.Forms))]
        public virtual Surface SpillSurface { get; set; }
        [ForeignKey(nameof(StuserId))]
        [InverseProperty("Forms")]
        public virtual Stuser Stuser { get; set; }
    }
}
