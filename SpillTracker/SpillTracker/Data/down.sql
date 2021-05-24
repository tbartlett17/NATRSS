ALTER TABLE Form DROP CONSTRAINT Form_FK_STUserID
ALTER TABLE Form DROP CONSTRAINT Form_FK_FacilityChemicalID
ALTER TABLE Form DROP CONSTRAINT Form_FK_Spill_SurfaceID
ALTER TABLE Form DROP CONSTRAINT Form_FK_ChemicalStateID
ALTER TABLE Form DROP CONSTRAINT Form_FK_FacilityID
ALTER TABLE STUser DROP CONSTRAINT STUser_FK_CompanyID
ALTER TABLE Facility DROP CONSTRAINT Facility_FK_CompanyID
ALTER TABLE FacilityChemicals DROP CONSTRAINT FacilityChemicals_FK_ChemicalStateID
ALTER TABLE FacilityChemicals DROP CONSTRAINT FacilityChemicals_FK_ChemicalID
ALTER TABLE FacilityChemicals DROP CONSTRAINT FacilityChemicals_FK_FacilityID
ALTER TABLE StuserFacilities DROP CONSTRAINT StuserFacilities_FK_StuserId
ALTER TABLE StuserFacilities DROP CONSTRAINT StuserFacilities_FK_FacilityId

DROP TABLE ChemicalState
DROP TABLE Surface
DROP TABLE Company
DROP TABLE STUser
DROP TABLE Chemical
DROP TABLE Facility
DROP TABLE FacilityChemicals
DROP TABLE Form
DROP TABLE ContactInfo
DROP TABLE StatusTime
DROP TABLE StuserFacilities
