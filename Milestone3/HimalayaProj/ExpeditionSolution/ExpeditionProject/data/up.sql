-- UP script for SQL Server

CREATE TABLE [Peak] (
  [ID]              INT PRIMARY KEY IDENTITY(1, 1),
  [Name]            NVARCHAR(30) NOT NULL,
  [Height]          INT NOT NULL,
  [ClimbingStatus]  BIT NOT NULL,
  [FirstAscentYear] INT
)
GO

CREATE TABLE [Expedition] (
  [ID]                INT PRIMARY KEY IDENTITY(1, 1),
  [Season]            NVARCHAR(10),
  [Year]              INT,
  [StartDate]         DATE,
  [TerminationReason] NVARCHAR(80),
  [OxygenUsed]        BIT,
  [PeakID]            INT,
  [TrekkingAgencyID]  INT,
)
GO

CREATE TABLE [TrekkingAgency] (
  [ID]    INT PRIMARY KEY IDENTITY(1, 1),
  [Name]  NVARCHAR(100)
)
GO

CREATE TABLE [Climber] (
	[ID]      INT PRIMARY KEY IDENTITY(1, 1),
	[FirstName]	  NVARCHAR(50) NOT NULL,
	[LastName]	  NVARCHAR(50) NOT NULL, 
	[BirthDate] DATE,
	[Age]	  INT,
  [ExpeditionID] INT
)
GO

CREATE TABLE [User] (
  [ID]          INT PRIMARY KEY IDENTITY(1, 1),
  [UserName]    NVARCHAR(20) NOT NULL,
  [Password]    NVARCHAR(25) NOT NULL,
  [Name]        NVARCHAR(50) NOT NULL,
  [BirthDate]   DATE,
  [UserTypeID]  INT
)
GO

CREATE TABLE [UserType] (
  [ID]          INT PRIMARY KEY IDENTITY(1, 1),
  [Role]        NVARCHAR(20) NOT NULL,
  [Privilage]   BIT
)
GO

CREATE TABLE [BlogPost] (
  [ID]          INT PRIMARY KEY IDENTITY(1, 1),
  [Post]        NVARCHAR(500),
  [UserID]      INT
)
GO

CREATE TABLE [Form] (
  [ID]          INT PRIMARY KEY IDENTITY(1, 1),
  [Description] NVARCHAR(500),
  [Status] NVARCHAR(20),
  [Completed] BIT,
  [ExpeditionID] INT,
  [UserID]      INT,
  [SubmissionDateTime] DATE
)
GO

CREATE TABLE [ExpeditionProvidertoExpeditions] (
  [ID]            INT PRIMARY KEY IDENTITY(1, 1),
  [ExpeditionID]  INT,
  [UserID]        INT
)


ALTER TABLE [Expedition] ADD CONSTRAINT [Expedition_FK_Peak] FOREIGN KEY ([PeakID]) REFERENCES [Peak] ([ID])
ALTER TABLE [Expedition] ADD CONSTRAINT [Expedition_FK_TrekkingAgency] FOREIGN KEY ([TrekkingAgencyID]) REFERENCES [TrekkingAgency] ([ID])
ALTER TABLE [Climber] ADD CONSTRAINT [Member_FK_Expedition] FOREIGN KEY ([ExpeditionID]) REFERENCES [Expedition] ([ID]) 	
ALTER TABLE [User] ADD CONSTRAINT [User_FK_UserType] FOREIGN KEY ([UserTypeID]) REFERENCES [UserType] ([ID])
ALTER TABLE [BlogPost] ADD CONSTRAINT [BlogPost_FK_User] FOREIGN KEY ([UserID]) REFERENCES [User] ([ID])
ALTER TABLE [Form] ADD CONSTRAINT [Form_FK_Expedition] FOREIGN KEY ([ExpeditionID]) REFERENCES [Expedition] ([ID])
ALTER TABLE [Form] ADD CONSTRAINT [Form_Fk_User] FOREIGN KEY ([UserID]) REFERENCES [User] ([ID])
ALTER TABLE [ExpeditionProvidertoExpeditions] ADD CONSTRAINT [EP_FK_Expedition] FOREIGN KEY ([ExpeditionID]) REFERENCES [Expedition] ([ID])
ALTER TABLE [ExpeditionProvidertoExpeditions] ADD CONSTRAINT [EP_Fk_User] FOREIGN KEY ([UserID]) REFERENCES [User] ([ID])
GO