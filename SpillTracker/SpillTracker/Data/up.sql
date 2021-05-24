CREATE TABLE [Form] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Spill_Reported_By] nvarchar(50),
  [Spill_Reported_Date] date,
  [Spill_Reported_Time] time,
  [Spill_Location] nvarchar(100),
  [Spill_Ongoing] bit,
  [Spill_Contained] bit,
  [Need_Assistance] bit,
  [Chemical_Pressurized] bit,
  [Spill_Volume] float,
  [Spill_Volume_Units] nvarchar(30),
  [Chemical_Concentration] float,
  [Spill_Forming_Puddle] bit,
  [Spill_Reach_Water_Source] bit,
  [Water_Source] nvarchar(100),
  [Spill_Duration_Hours] float,
  [Spill_Duration_Minutes] float,
  [Cleanup_StartTime] datetime,
  [Chemical_Temperature] float,
  [Chemical_Temperature_Units] nvarchar(30),
  [Spill_Width] float,
  [Spill_Width_Units] nvarchar(30),
  [Spill_Length] float,
  [Spill_Length_Units] nvarchar(30),
  [Spill_Depth] float,
  [Spill_Depth_Units] nvarchar(30),
  [Spill_Area] float,
  [Spill_Area_Units] nvarchar(30),
  [Spill_Reportable] bit,
  [Wind_Direction] nvarchar(10),
  [Wind_Speed] float,
  [Wind_Speed_Units] nvarchar(15),
  [Address_Street] nvarchar(100),
  [Address_City] nvarchar(100),
  [Address_State] nvarchar(50),
  [Address_ZIP] nvarchar(15),
  [Weather_Temperature] float,
  [Weather_Temperature_Units] nvarchar(30),
  [Weather_Humidity] float,
  [Weather_Humidity_Units] nvarchar(15),
  [Sky_Conditions] nvarchar(50),
  [Spill_Evaporation_Rate] float,
  [Spill_Evaporation_Rate_Units] nvarchar(10),
  [Amount_Evaporated] float,
  [Amount_Evaporated_Units] nvarchar(10),
  [Amount_Spilled] float,
  [Amount_Spilled_Units] nvarchar(10),
  [Notes] nvarchar(500),
  [ContactNotes] nvarchar(500),
  [STUserID] int,
  [FacilityChemicalID] int,
  [Spill_SurfaceID] int,
  [ChemicalStateID] int,
  [FacilityID] int
)
GO

CREATE TABLE [STUser] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [ASPNetIdentityID] nvarchar(450),
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [EmployeeNumber] nvarchar(25),
  [CompanyID] int
)
GO

CREATE TABLE [Chemical] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(300),
  [Aliases] nvarchar(max),
  [CAS_Num] nvarchar(150),
  [PubChemCID] int,
  [Reportable_Quantity] float,
  [Reportable_Quantity_Units] nvarchar(30),
  [Density] float,
  [Density_Units] nvarchar(30),
  [Molecular_Weight] float,
  [Molecular_Weight_Units] nvarchar(30),
  [Vapor_Pressure] float,
  [Vapor_Pressure_Units] nvarchar(30),
  [CERCLA_Chem] bit,
  [EPCRA_Chem] bit
)
GO

CREATE TABLE [Surface] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Type] nvarchar(25)
)
GO

CREATE TABLE [ChemicalState] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Type] nvarchar(25)
)
GO

CREATE TABLE [Facility] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(100),
  [Address_Street] nvarchar(100),
  [Address_City] nvarchar(100),
  [Address_State] nvarchar(50),
  [Address_ZIP] nvarchar(15),
  [Location] nvarchar(100),
  [Industry] nvarchar(50),
  [AccessCode] nvarchar(20),
  [CompanyID] int
)
GO

CREATE TABLE [FacilityChemicals] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Concentration] float,
  [Chemical_Temperature] float,
  [Chemical_Temperature_Units] nvarchar(30),
  [ChemicalStateID] int,
  [ChemicalID] int,
  [FacilityID] int
)
GO

CREATE TABLE [ContactInfo] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [AgencyName] nvarchar(150),
  [PhoneNumber] nvarchar(20),
  [State] nvarchar(35)
)
GO

CREATE TABLE [Company] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(100),
  [AccessCode] nvarchar(20),
  [Num_Facilities] int
)
GO

CREATE TABLE [StatusTime] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [SourceName] nvarchar(100),
  [Time] datetime
)
GO

CREATE TABLE [StuserFacilities] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [StuserID] int,
  [FacilityID] int
)
GO

ALTER TABLE [Form] ADD CONSTRAINT Form_FK_STUserID FOREIGN KEY ([STUserID]) REFERENCES [STUser] ([ID])
GO

ALTER TABLE [Form] ADD CONSTRAINT  Form_FK_FacilityChemicalID FOREIGN KEY ([FacilityChemicalID]) REFERENCES [FacilityChemicals] ([ID])
GO

ALTER TABLE [Form] ADD CONSTRAINT Form_FK_Spill_SurfaceID FOREIGN KEY ([Spill_SurfaceID]) REFERENCES [Surface] ([ID])
GO

ALTER TABLE [Form] ADD CONSTRAINT Form_FK_ChemicalStateID FOREIGN KEY ([ChemicalStateID]) REFERENCES [ChemicalState] ([ID])
GO

ALTER TABLE [Form] ADD CONSTRAINT Form_FK_FacilityID FOREIGN KEY ([FacilityID]) REFERENCES [Facility] ([ID])
GO

ALTER TABLE [STUser] ADD CONSTRAINT STUser_FK_CompanyID FOREIGN KEY ([CompanyID]) REFERENCES [Company] ([ID])
GO

ALTER TABLE [Facility] ADD CONSTRAINT Facility_FK_CompanyID FOREIGN KEY ([CompanyID]) REFERENCES [Company] ([ID])
GO

ALTER TABLE [FacilityChemicals] ADD CONSTRAINT FacilityChemicals_FK_ChemicalStateID FOREIGN KEY ([ChemicalStateID]) REFERENCES [ChemicalState] ([ID])
GO

ALTER TABLE [FacilityChemicals] ADD CONSTRAINT FacilityChemicals_FK_ChemicalID FOREIGN KEY ([ChemicalID]) REFERENCES [Chemical] ([ID])
GO

ALTER TABLE [FacilityChemicals] ADD CONSTRAINT FacilityChemicals_FK_FacilityID FOREIGN KEY ([FacilityID])  REFERENCES [Facility] ([ID])
GO

ALTER TABLE [StuserFacilities] ADD CONSTRAINT StuserFacilities_FK_StuserId FOREIGN KEY ([StuserID]) REFERENCES [Stuser] ([ID])
GO

ALTER TABLE [StuserFacilities] ADD CONSTRAINT StuserFacilities_FK_FacilityId FOREIGN KEY ([FacilityID]) REFERENCES [Facility] ([ID])
GO
