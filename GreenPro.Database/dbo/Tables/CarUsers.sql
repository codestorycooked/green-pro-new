CREATE TABLE [dbo].[CarUsers]
(
	[CarId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DisplayName] NVARCHAR(50) NOT NULL, 
    [Make] NVARCHAR(50) NOT NULL, 
    [LicenseNumber] NVARCHAR(50) NOT NULL, 
    [Color] NVARCHAR(50) NULL, 
    [Type] INT NOT NULL, 
	[PurchaseYear] int NOT NULL,
    [UserId] NVARCHAR(128) NULL, 
    [AutoRenewal] BIT NOT NULL DEFAULT 0, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [GarageId] INT NULL, 
    CONSTRAINT [FK_CarUsers_Users] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_CarUsers_CarTypes] FOREIGN KEY ([Type]) REFERENCES [CarTypes]([Id]),
	CONSTRAINT [FK_CarUsers_Garage] FOREIGN KEY ([GarageId]) REFERENCES [Garages]([GarageId])
)
