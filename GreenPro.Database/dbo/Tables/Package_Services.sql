CREATE TABLE [dbo].[Package_Services]
(
	[PackageServiceId] INT NOT NULL PRIMARY KEY Identity, 
    [PackageID] INT NOT NULL, 
    [ServiceID] INT NOT NULL, 
   
    CONSTRAINT [FK_Package_Services_Packages] FOREIGN KEY ([PackageID]) REFERENCES Packages(PackageId), 
    CONSTRAINT [FK_Package_Services_Services] FOREIGN KEY ([ServiceID]) REFERENCES Services(ServiceID)
)
