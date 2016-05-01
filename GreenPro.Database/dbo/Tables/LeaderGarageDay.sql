CREATE TABLE [dbo].[LeaderGarageDay]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	WorkerGarageID int not null,
	Day nvarchar(50) not null, 
    CONSTRAINT [FK_LeaderGarageDay_WorkerGarages] FOREIGN KEY (WorkerGarageID) REFERENCES WorkerGarages(ID)
)
