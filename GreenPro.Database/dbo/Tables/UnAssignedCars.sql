CREATE TABLE [dbo].[UnAssignedCars]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [GarageID] INT NOT NULL, 
    [PackageId] INT NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_UnAssignedCars_Garages] FOREIGN KEY (GarageID) REFERENCES Garages(GarageID), 
    CONSTRAINT [FK_UnAssignedCars_Packages] FOREIGN KEY ([PackageId]) REFERENCES UserPackages(Id)
)
