CREATE TABLE [dbo].[WorkLogDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Comment] NVARCHAR(MAX) NOT NULL, 
    [SideId] INT NOT NULL, 
    [PrePost] NVARCHAR(50) NOT NULL, 
    [ImageRef] NVARCHAR(MAX) NOT NULL, 
	[WorkDoneId] INT NOT NULL, 
	CONSTRAINT [FK_WorkLogDetails_WorkDone] FOREIGN KEY ([WorkDoneId]) REFERENCES [WorkDone]([Id]),
    CONSTRAINT [FK_WorkLogDetails_LogDetailCarSide] FOREIGN KEY ([SideId]) REFERENCES [LogDetailCarSide]([Id]) 
)
