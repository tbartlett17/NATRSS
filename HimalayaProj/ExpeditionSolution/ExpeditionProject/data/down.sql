-- DOWN script for SQL Server

ALTER TABLE [Expedition] DROP CONSTRAINT [Expedition_FK_Peak];
ALTER TABLE [Expedition] DROP CONSTRAINT [Expedition_FK_TrekkingAgency];
ALTER TABLE [CLimber] DROP CONSTRAINT [Climber_FK_Expedition];
ALTER TABLE [User] DROP CONSTRAINT [User_FK_UserType];
ALTER TABLE [BlogPost] DROP CONSTRAINT [BlogPost_FK_User];
ALTER TABLE [Form] DROP CONSTRAINT [Form_FK_Expedition];
ALTER TABLE [Form] DROP CONSTRAINT [Form_Fk_User];
-- ALTER TABLE [] DROP CONSTRAINT [];

DROP TABLE [Expedition];
DROP TABLE [Peak];
DROP TABLE [TrekkingAgency];
DROP TABLE [Climber];
DROP TABLE [User];
DROP TABLE [UserType];
DROP TABLE [BlogPost];
DROP TABLE [Form]