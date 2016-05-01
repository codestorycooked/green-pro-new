CREATE TABLE [dbo].[Logs]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [source] NVARCHAR(256) NULL, 
    [InnerException] NTEXT NULL, 
    [StackStrace] NTEXT NULL, 
    [Message] NTEXT NULL, 
    [LogDate] DATETIME NULL
)
