INSERT INTO [Company] (Name,AccessCode,Num_Facilities) VALUES
    ('Test Company 1','abcdefghi1',1),
    ('Test Company 2','abcdefghi12',2);


INSERT INTO [Facility] (Name,Address_Street,Address_City,Address_State,Address_ZIP,Location,Industry,AccessCode,CompanyID) VALUES
    ('Test Facilty 1','123 Main St','Monmouth','OR','97361','44.848588, -123.236404','Metals','fac-abcdefghi-1',1),  
    ('Test Facilty 2','55 SW 2nd St','Monmouth','OR','97361','44.861185, -123.251233','Fabrics','fac-abcdefghi-2',2),
    ('Test Facilty 3','123 Main St','Monmouth','OR','97361','44.654095, -122.001693','Metals','fac-abcdefghi-3',1);


INSERT INTO [STUser](ASPNetIdentityID,FirstName,LastName,EmployeeNumber,CompanyID) VALUES 
    ('','Bob','Ross','A1',1);


INSERT INTO [Chemical] (Name,Aliases,CAS_Num,PubChemCID,Reportable_Quantity,Reportable_Quantity_Units,Density,Density_Units,Molecular_Weight,Molecular_Weight_Units,Vapor_Pressure,Vapor_Pressure_Units,CERCLA_Chem,EPCRA_Chem) VALUES
    ('Acetone Cyanohydrin','','75-86-5',6406,10,'lbs',0.9267,'g/cm³',85.1,'g/mol',0.8,'mm Hg',0,1),
    ('Acetone Thiosemicarbazide','','1752-30-3',2770166,1000,'lbs',NULL,'g/cm³',131.2,'g/mol',NULL,'mm Hg',0,1), -- missing density, vapor pressure. setting to 0. info not available on PubChem. need a case for when this occurs or to keep track of it
    ('Acrolein','','107-02-8',7847,1,'lbs',0.8389,'g/cm³',56.06,'g/mol',135.71,'mm Hg',0,1),
    ('Acrylamide','','79-06-1',6579,5000,'lbs',1.122,'g/cm³',71.08,'g/mol',0.007,'mm Hg',0,1);


INSERT INTO [ChemicalState] (Type) VALUES
    ('Solid'),
    ('Liquid'),
    ('Gas');


INSERT INTO [FacilityChemicals](Concentration,Chemical_Temperature,Chemical_Temperature_Units,ChemicalStateID,ChemicalID,FacilityID) VALUES
    (50,70,'°F',2,1,1),
    (75,70,'°F',2,2,1),
    (20,70,'°F',2,3,2),
    (32,70,'°F',2,4,2),
    (25,70,'°F',2,1,3),
    (70,70,'°F',2,4,3);


INSERT INTO [Surface] (Type) VALUES
    ('Gravel'),
    ('Soil'),
    ('Sand'),
    ('Asphalt'),
    ('Concrete');


-- storing time duration in minutes
INSERT INTO [Form] (Spill_Reported_By,Spill_Reported_Date,Spill_Reported_Time,Spill_Location,Spill_Ongoing,Spill_Contained,Need_Assistance,Chemical_Pressurized,Spill_Volume,Spill_Volume_Units,Chemical_Concentration,Spill_Forming_Puddle,Spill_Reach_Water_Source,Water_Source,Spill_Duration_Hours,Spill_Duration_Minutes,Cleanup_StartTime,Chemical_Temperature,Chemical_Temperature_Units,Spill_Width,Spill_Width_Units,Spill_Length,Spill_Length_Units,Spill_Depth,Spill_Depth_Units,Spill_Area,Spill_Area_Units,Spill_Reportable,Wind_Direction,Wind_Speed,Wind_Speed_Units,Address_Street,Address_City,Address_State,Address_ZIP,Weather_Temperature,Weather_Temperature_Units,Weather_Humidity,Weather_Humidity_Units,Sky_Conditions,Spill_Evaporation_Rate,Spill_Evaporation_Rate_Units,Amount_Evaporated,Amount_Evaporated_Units,Amount_Spilled,Amount_Spilled_Units,Notes,ContactNotes,STUserID,FacilityChemicalID,Spill_SurfaceID,ChemicalStateID,FacilityID) VALUES
    ('Joe Smith','2020-12-02','14:07:00','LOCATION',0,1,0,0,2,'gal',0.36,1,0,NULL,01,35,'2020-12-02 14:20:00',77,'F',3,'ft',4,'ft',NULL,NULL,12,'ft\u2072',0,'NE',6,'MPH','ADDRESS','CITY','STATE','ZIP',77,'F',45,'%','Cloudy',0.0017,'lbs/min',0.0035,'lbs',0.00,'lbs','','',1,1,4,2,1); -- no liquid escaped site, rlease to air is 0.0035 lbs, not reportable

INSERT INTO [ContactInfo] (AgencyName,PhoneNumber,State) VALUES 
    ('National Response Center','1-800-424-8802','Federal'),
    ('Oregon Department of Environmental Quality','1-503-378-8240','Oregon');

INSERT INTO [StatusTime] (SourceName, Time) VALUES ('EPCRA Scraper','2021-2-28 05:00:00')
INSERT INTO [StatusTime] (SourceName, Time) VALUES ('CERCLA Scraper','2021-2-28 05:00:00')