CREATE TABLE [dbo].[LeaderCarJob]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Day] NVARCHAR(50) NOT NULL, 
    [GarageId] INT NOT NULL, 
    [LeaderId] NVARCHAR(128) NOT NULL, 
    [CarId] INT NOT NULL,
	CONSTRAINT [FK_LeaderCarJob_Garages] FOREIGN KEY (GarageID) REFERENCES Garages(GarageID), 
    CONSTRAINT [FK_LeaderCarJob_AspNetUser] FOREIGN KEY ([LeaderId]) REFERENCES [AspNetUsers]([Id]),
	CONSTRAINT [FK_LeaderCarJob_CarUser] FOREIGN KEY ([CarId]) REFERENCES [CarUsers]([CarId])
)
