CREATE TABLE [dbo].[CarServiceEntries]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CarOwnerId] NVARCHAR(128) NOT NULL, 
    [CarId] INT NOT NULL, 
    [WorkerId] NVARCHAR(128) NOT NULL, 
    [PackageId] INT NOT NULL, 
    [Comments] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_CarOwnerId_AspNetUsers] FOREIGN KEY ([CarOwnerId]) REFERENCES [AspNetUsers]([Id]),
	CONSTRAINT [FK_WorkererId_AspNetUsers] FOREIGN KEY ([WorkerId]) REFERENCES [AspNetUsers]([Id]),
	CONSTRAINT [FK_CarServiceEntries_CarUsers] FOREIGN KEY ([CarId]) REFERENCES [CarUsers]([CarId]),
	CONSTRAINT [FK_CarServiceEntries_Packages] FOREIGN KEY ([PackageId]) REFERENCES [Packages]([PackageId])
)
