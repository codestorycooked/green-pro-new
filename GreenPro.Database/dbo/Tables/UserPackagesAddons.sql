CREATE TABLE [dbo].[UserPackagesAddons]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserPackageID] INT NOT NULL, 
    [ServiceID] INT NOT NULL, 
    [ActualPrice] MONEY NOT NULL, 
    [DiscountPrice] MONEY NOT NULL, 
    [CreatedDt] DATETIME NULL, 
    CONSTRAINT [FK_UserPackagesAddons_UserPackages] FOREIGN KEY (UserPackageID) REFERENCES UserPackages(ID), 
    CONSTRAINT [FK_UserPackagesAddons_ToServices] FOREIGN KEY (ServiceID) REFERENCES [Services](ServiceID)

)
