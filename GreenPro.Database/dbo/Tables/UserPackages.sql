CREATE TABLE [dbo].[UserPackages]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [PackageId] INT NOT NULL, 
	[CarId] INT NOT NULL,
    [SubscribedDate] DATETIME NOT NULL, 
    [ActualPrice] MONEY NOT NULL, 
    [TotalPrice] MONEY NOT NULL, 
    [PriceWithAddOns] MONEY NOT NULL, 
    [DiscountPrice] MONEY NOT NULL, 
    [Ipaddress] NVARCHAR(20) NULL, 
    [CreatedDt] DATETIME NOT NULL, 
    [PaymentRecieved] BIT NOT NULL DEFAULT 0, 
    [Processed] BIT NOT NULL DEFAULT 0, 
    [TaxAmount] MONEY NOT NULL DEFAULT 0, 
    [TipAmount] MONEY NOT NULL DEFAULT 0, 
    [SubscriptionEndDate] DATETIME NULL, 
    CONSTRAINT [FK_UserPackages_Users] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_UserPackages_Packages] FOREIGN KEY ([PackageId]) REFERENCES [Packages]([PackageId]), 
    CONSTRAINT [FK_UserPackages_CarUsers] FOREIGN KEY ([CarId]) REFERENCES [CarUsers]([CarId])
)
