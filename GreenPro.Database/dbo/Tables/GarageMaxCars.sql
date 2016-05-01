CREATE TABLE [dbo].[GarageMaxCars]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [GarageID] INT NOT NULL, 
    [CarPerCrewAdmin] INT NOT NULL, 
    [DayRef] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_GarageMaxCars_Garages] FOREIGN KEY (GarageID) REFERENCES Garages(GarageID) 
)
