CREATE TABLE [dbo].[WorkerGarages]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [CrewLeaderId] NVARCHAR(128) NOT NULL, 
    [GarageID] INT NOT NULL, 
    [IsLeader] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_WorkerGarages_Garages] FOREIGN KEY (GarageID) REFERENCES Garages(GarageID), 
    CONSTRAINT [FK_WorkerGarages_AspNetUser] FOREIGN KEY ([CrewLeaderId]) REFERENCES [AspNetUsers]([Id])
)
