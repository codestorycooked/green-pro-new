/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[States] ON
INSERT INTO [dbo].[States] ([Id], [StateName]) VALUES (1, N'New York')
SET IDENTITY_INSERT [dbo].[States] OFF
SET IDENTITY_INSERT [dbo].[CITIES] ON
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID], [IsActive]) VALUES (1, N'New York', 1, 1)
SET IDENTITY_INSERT [dbo].[CITIES] OFF

INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance], [IsDelete], [ProfilePic]) VALUES (N'1b1d1602-39d1-4b1b-a09f-15f4136a8753', N'admin@greenpro.com', 0, N'AKZwo0PQJyX9bsYm5RhfozLmzErvhUOhQ4z3l8KlAnf1G1VkIgrljo1rmBUBWyWYlg==', N'7e88ae30-b4e5-4b7e-82ef-6aba14a16442', NULL, 0, 0, NULL, 0, 0, N'admin@greenpro.com', N'admin@greenpro.com', NULL, NULL, NULL, 1, 1, NULL, 0, 0, NULL)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance], [IsDelete], [ProfilePic]) VALUES (N'98ed1db3-070f-47d6-9c01-9b8f4d95b4c5', N'kunal@innoator.com', 0, N'AH25v9EpbMH2QIwrk2w9PqzkSZuLJwrTXsEppO/jEpWwEJ7ZPIbuTqheCVX7uSQehw==', N'6e416d31-b1aa-4c43-b0b7-a467c6d2552b', NULL, 0, 0, NULL, 0, 0, N'kunal@innoator.com.com', N'kunal', NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL)
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'467fe956-96e0-44db-9de1-21a0216823ec', N'Admin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'195b878a-38b0-48ee-bcd8-874bfcad4564', N'Crew Admin')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1b1d1602-39d1-4b1b-a09f-15f4136a8753', N'467fe956-96e0-44db-9de1-21a0216823ec')


SET IDENTITY_INSERT [dbo].[Garages] ON
INSERT INTO [dbo].[Garages] ([GarageId], [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES (1, N'CENTURY PARKING CORP.', N'2600 Netherland Ave, Riverdale, Bronx, NY 10463', 1, 1, N'US', N'10463', 40.8790678, -73.9150773, 1, N'2017-01-15', N'Admin', N'Person', N'989', N'email@email.com', N'09:00:00', N'18:00:00', N'Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday')
INSERT INTO [dbo].[Garages] ([GarageId], [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES (2, N'CONCOURSE PARKING CORP.', N'771 Concourse Village West, Bronx, NY, United States', 1, 1, N'US', N'412101', 40.8235792, -73.9231025, 1, N'2017-01-15', N'Admin', N'Kunal Deshmukh', N'9860766659', N'kunal.deshmukh@hotmail.com', N'09:00:00', N'12:00:00', N'Monday,Tuesday,Wednesday')
INSERT INTO [dbo].[Garages] ([GarageId], [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES (3, N'HUDSON RIVERPARKING CORP.', N'3718 Henry Hudson Parkway, Riverdale, Bronx, NY 10463', 1, 1, N'US', N'10463', 40.8882435, -73.91004, 1, N'2017-01-15', N'Admin', N'Kunal Deshmukh', N'9860766659', N'kunal.deshmukh@hotmail.com', N'09:00:00', N'20:00:00', N'Sunday,Monday,Thursday,Friday')
INSERT INTO [dbo].[Garages] ([GarageId], [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES (4, N'BLACKSTONE PARKING CORP.', N'3950 Blackstone Avenue, Bronx, NY 10471', 1, 1, N'US', N'10471', 40.8905231, -73.9105572, 1, N'2017-01-15', N'Admin', N'Kunal Deshmukh', N'9860766659', N'kunal.deshmukh@hotmail.com', N'09:00:00', N'20:00:00', N'Sunday,Monday,Wednesday')
SET IDENTITY_INSERT [dbo].[Garages] OFF


SET IDENTITY_INSERT [dbo].[Services] ON
INSERT INTO [dbo].[Services] ([ServiceID], [Service_Name], [Service_Description], [Service_Price], [IsAddOn], [CreateDt], [CreatedBy]) VALUES (1, N'Tyre Cleaning', N'Tyre Cleaning Service', CAST(10.0000 AS Money), 1, N'2017-01-15 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([ServiceID], [Service_Name], [Service_Description], [Service_Price], [IsAddOn], [CreateDt], [CreatedBy]) VALUES (2, N'Mirror Cleaning', N'Mirror Cleaning Service', CAST(5.0000 AS Money), 1, N'2017-01-15 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([ServiceID], [Service_Name], [Service_Description], [Service_Price], [IsAddOn], [CreateDt], [CreatedBy]) VALUES (3, N'Interior Cleaning', N'Interior Cleaning Service', CAST(15.0000 AS Money), 1, N'2017-01-15 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([ServiceID], [Service_Name], [Service_Description], [Service_Price], [IsAddOn], [CreateDt], [CreatedBy]) VALUES (4, N'Car Detailing', N'Car Detailing Servic', CAST(20.0000 AS Money), 1, N'2017-01-15 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([ServiceID], [Service_Name], [Service_Description], [Service_Price], [IsAddOn], [CreateDt], [CreatedBy]) VALUES (5, N'Upholstery Cleaning', N'Upholstery Cleaning Services', CAST(8.0000 AS Money), 1, N'2017-01-15 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([ServiceID], [Service_Name], [Service_Description], [Service_Price], [IsAddOn], [CreateDt], [CreatedBy]) VALUES (6, N'Car Wash', N'Water less exterior Wash', CAST(5.0000 AS Money), 1, N'2017-01-15 00:00:00', N'Admin')
SET IDENTITY_INSERT [dbo].[Services] OFF

SET IDENTITY_INSERT [dbo].[Packages] ON
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [Package_Price], [CreateDt], [CreatedBy], [SubscriptionTypes]) VALUES (1, N'Gold', N'Basic Package for Exterior Cleaning (Waterless Cleaning)', CAST(10.0000 AS Money), N'2017-01-15 13:38:33', N'Admin', N'4,1,2,3')
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [Package_Price], [CreateDt], [CreatedBy], [SubscriptionTypes]) VALUES (2, N'Silver', N'Basic Package for Exterior Cleaning (Waterless Cleaning)', CAST(15.0000 AS Money), N'2017-01-15 13:38:59', N'Admin', N'4,1,2,3')
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [Package_Price], [CreateDt], [CreatedBy], [SubscriptionTypes]) VALUES (3, N'Diamond', N'Basic Package for Exterior Cleaning (Waterless Cleaning)', CAST(30.0000 AS Money), N'2017-01-15 13:39:24', N'Admin', N'4,1,2,3')
SET IDENTITY_INSERT [dbo].[Packages] OFF

SET IDENTITY_INSERT [dbo].[Package_Services] ON
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (1, 1, 6)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (2, 2, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (3, 2, 6)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (4, 3, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (5, 3, 2)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (6, 3, 6)
SET IDENTITY_INSERT [dbo].[Package_Services] OFF


GO
