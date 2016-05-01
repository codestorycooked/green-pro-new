CREATE TABLE [dbo].[Packages]
(
	[PackageId] INT NOT NULL PRIMARY KEY Identity, 
    [Package_Name] NVARCHAR(100) NOT NULL, 
    [Package_Description] NVARCHAR(255) NOT NULL, 
	[CarTypeId] int not null,
    [Package_Price] MONEY NOT NULL, 
    [CreateDt] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Packages_CarTypes] FOREIGN KEY ([CarTypeId]) REFERENCES [CarTypes]([Id])   
)
