CREATE TABLE [dbo].[WorkDone]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CrewLeaderID] NVARCHAR(128) NOT NULL, 
    [CrewMemberId] NVARCHAR(128) NOT NULL, 
    [PackageId] INT NOT NULL, 
    [StartTimeStamp] DATETIME NULL, 
    [EndTimeStamp] DATETIME NULL, 
    [IsCompleted] BIT NOT NULL DEFAULT 0, 
    [CreatedTimeStamp] DATETIME NULL,
	[UserId] NVARCHAR(128) NULL, 
    CONSTRAINT [FK_WorkDone_AspNetUsers1] FOREIGN KEY ([CrewLeaderID]) REFERENCES [AspNetUsers]([Id]) ,
	CONSTRAINT [FK_WorkDone_AspNetUsers2] FOREIGN KEY ([CrewMemberId]) REFERENCES [AspNetUsers]([Id]) ,
	
	CONSTRAINT [FK_WorkDone_Packages] FOREIGN KEY ([PackageId]) REFERENCES [Packages]([PackageId])
)
