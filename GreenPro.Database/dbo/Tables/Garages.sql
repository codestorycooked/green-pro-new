CREATE TABLE [dbo].[Garages]
(
	[GarageId] INT NOT NULL PRIMARY KEY identity,
	Garage_Name nvarchar(100) not null ,
	Garage_Address nvarchar(255) not null,
	State INT not null,
	City INT not null,
	Country nvarchar(50) not null,
	Pincode nvarchar(10) not null,
	Latitute FLOAT not null,
	Longitude FLOAT not null,
	IsActive bit ,
	CreatedDt date not null,
	CreatedBy nvarchar(50) not null, 
    [Contact_Person] NVARCHAR(100) NOT NULL, 
    [Phone_Number] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [OpenTime] TIME NULL, 
    [CloseTime] TIME NOT NULL, 
    [ServiceDays] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Garages_States] FOREIGN KEY ([State]) REFERENCES [States]([Id]), 
    CONSTRAINT [FK_Garages_Cities] FOREIGN KEY ([City]) REFERENCES [Cities]([Id]) 


	
)
