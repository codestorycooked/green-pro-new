CREATE TABLE [dbo].[Services]
(
	[ServiceID] INT NOT NULL PRIMARY KEY Identity, 
    [Service_Name] NVARCHAR(100) NOT NULL, 
    [Service_Description] NVARCHAR(200) NOT NULL, 
    [Service_Price] MONEY NOT NULL, 
    [CreateDt] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(50) NOT NULL
)
