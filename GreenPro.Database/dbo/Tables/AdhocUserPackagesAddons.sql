CREATE TABLE [dbo].[AdhocUserPackagesAddons]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserPackageID] INT NOT NULL, 
    [ServiceID] INT NOT NULL, 
    [ActualPrice] MONEY NOT NULL, 
    [DiscountPrice] MONEY NOT NULL, 
    [CreatedDt] DATETIME NULL, 
    CONSTRAINT [FK_AdhocUserPackagesAddons_AdhocUserPackages] FOREIGN KEY (UserPackageID) REFERENCES AdhocUserPackages(ID), 
    CONSTRAINT [FK_AdhocUserPackagesAddons_ToServices] FOREIGN KEY (ServiceID) REFERENCES [Services](ServiceID)

)
