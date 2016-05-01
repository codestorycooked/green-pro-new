﻿/*
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
INSERT INTO [dbo].[States] ([Id], [StateName]) VALUES (1, N'New Jersey')
INSERT INTO [dbo].[States] ([Id], [StateName]) VALUES (2, N'New York')
SET IDENTITY_INSERT [dbo].[States] OFF

SET IDENTITY_INSERT [dbo].[Cities] ON
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (1, N'Atlantic', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (2, N'Bergen', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (3, N'Burlington', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (4, N'Camden', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (5, N'Cape May', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (6, N'Cumberland', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (7, N'Essex', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (8, N'Gloucester', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (9, N'Hudson', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (10, N'Hunterdon', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (11, N'Mercer', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (12, N'Middlesex', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (13, N'Monmouth', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (14, N'Morris', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (15, N'Ocean', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (16, N'Passaic', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (17, N'Salem', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (18, N'Somerset', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (19, N'Sussex', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (20, N'Union', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (21, N'Warren', 1)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (22, N'Albany', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (23, N'Allegany', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (24, N'Bronx', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (25, N'Broome', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (26, N'Cattaraugus', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (27, N'Cayuga', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (28, N'Chautauqua', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (29, N'Chemung', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (30, N'Chenango', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (31, N'Clinton', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (32, N'Columbia', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (33, N'Cortland', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (34, N'Delaware', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (35, N'Dutchess', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (36, N'Erie', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (37, N'Essex', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (38, N'Franklin', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (39, N'Fulton', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (40, N'Genesee', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (41, N'Greene', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (42, N'Hamilton', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (43, N'Herkimer', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (44, N'Jefferson', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (45, N'Kings', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (46, N'Lewis', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (47, N'Livingston', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (48, N'Madison', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (49, N'Monroe', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (50, N'Montgomery', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (51, N'Nassau', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (52, N'New York', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (53, N'Niagara', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (54, N'Oneida', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (55, N'Onondaga', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (56, N'Ontario', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (57, N'Orange', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (58, N'Orleans', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (59, N'Oswego', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (60, N'Otsego', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (61, N'Putnam', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (62, N'Queens', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (63, N'Rensselaer', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (64, N'Richmond', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (65, N'Rockland', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (66, N'Saint Lawrence', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (67, N'Saratoga', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (68, N'Schenectady', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (69, N'Schoharie', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (70, N'Schuyler', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (71, N'Seneca', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (72, N'Steuben', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (73, N'Suffolk', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (74, N'Sullivan', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (75, N'Tioga', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (76, N'Tompkins', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (77, N'Ulster', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (78, N'Warren', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (79, N'Washington', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (80, N'Wayne', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (81, N'Westchester', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (82, N'Wyoming', 2)
INSERT INTO [dbo].[Cities] ([Id], [CityName], [StateID]) VALUES (83, N'Yates', 2)
SET IDENTITY_INSERT [dbo].[Cities] OFF


INSERT INTO [dbo].[CarTypes] ( [Description]) VALUES ( N'Two Seat Coupe')
INSERT INTO [dbo].[CarTypes] ( [Description]) VALUES ( N'Sedan')
INSERT INTO [dbo].[CarTypes] ( [Description]) VALUES ( N'Small SUV')
INSERT INTO [dbo].[CarTypes] ( [Description]) VALUES ( N'Large SUV / MINI VAN')
INSERT INTO [dbo].[CarTypes] ( [Description]) VALUES ( N'Extra Large Vehicles')


INSERT INTO [dbo].[Services] ([Service_Name], [Service_Description], [Service_Price], [CreateDt], [CreatedBy]) VALUES (N'Regular Exterior Wash', N'Regular Exterior Wash', CAST(10.0000 AS Money), N'2015-05-26 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([Service_Name], [Service_Description], [Service_Price], [CreateDt], [CreatedBy]) VALUES (N'Wipe Door Jams and Trunk Edges', N'Wipe Door Jams and Trunk Edges', CAST(10.0000 AS Money), N'2014-12-12 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([Service_Name], [Service_Description], [Service_Price], [CreateDt], [CreatedBy]) VALUES (N'Clean Mirrors', N'Clean Mirrors', CAST(5.0000 AS Money), N'2015-05-26 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([Service_Name], [Service_Description], [Service_Price], [CreateDt], [CreatedBy]) VALUES (N'Interior and Exterior Window Cleaning', N'Interior and Exterior Window Cleaning', CAST(15.0000 AS Money), N'2015-05-26 00:00:00', N'Admin')
INSERT INTO [dbo].[Services] ([Service_Name], [Service_Description], [Service_Price], [CreateDt], [CreatedBy]) VALUES (N'Clean Wheels and Tyres', N'Clean Wheels and Tyres', CAST(12.0000 AS Money), N'2015-05-26 00:00:00', N'Admin')



INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES ( N'Icon Parking Systems', N'350 Fifth Avenue, 34th floor
New York, NY 10118-3299 USA', 1, 1, N'US', N'121212', 40.748384, -73.9854792, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')
INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES ( N'Rapid Park Industries', N'59 W 36th St
New York, NY, United States', 1, 1, N'US', N'121212', 40.751095, -73.985919, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')
INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES ( N'Imperial Parking Systems', N'1501 Lexington Ave
New York, NY, United States', 1, 1, N'US', N'121212', 40.7863301, -73.9502706, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')
INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES ( N'TGS Garages & Doors', N'Farmingdale, NJ
United States', 1, 1, N'US', N'121212', 40.1965018, -74.1684757, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')
INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES ( N'Kingston Garage', N'9 Diamond Hill Rd
Marlboro Township, NJ, United States', 1, 1, N'US', N'121212', 40.29182, -74.235146, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')
INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES (N'EncoreGarage of New Jersey', N'1151 NJ-33
Farmingdale, NJ, United States', 1, 1, N'US', N'121212', 40.2255633, -74.1754369, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')
INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES (N'EncoreGarage of New Jersey', N'843 NJ-12
Frenchtown, NJ, United States', 1, 1, N'US', N'121212', 40.5098305, -74.9672936, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')
INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES (N'Garage Floor Coating of New Jersey', N'420 Benigno Blvd
Bellmawr, NJ, United States', 1, 1, N'US', N'121212', 39.8587001, -75.095471, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')
INSERT INTO [dbo].[Garages] ( [Garage_Name], [Garage_Address], [State], [City], [Country], [Pincode], [Latitute], [Longitude], [IsActive], [CreatedDt], [CreatedBy], [Contact_Person], [Phone_Number], [Email], [OpenTime], [CloseTime], [ServiceDays]) VALUES (N'New Jersey Garage Sales', N'4 Tamarack Ln
Pine Brook, NJ, United States', 1, 1, N'US', N'121212', 40.8663167, -74.3342012, 1, N'2015-05-18', N'ADmin', N'Kunal', N'09860766659', N'kunal@innoator.com', N'09:00:00', N'19:00:00', N'Tuesday')


SET IDENTITY_INSERT [dbo].[Packages] ON
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [CarTypeId], [Package_Price], [CreateDt], [CreatedBy]) VALUES (1, N'Basic Wash', N'This is Basic PAckage', 1, CAST(12.0000 AS Money), N'2015-06-18 18:54:57', N'Admin')
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [CarTypeId], [Package_Price], [CreateDt], [CreatedBy]) VALUES (2, N'GOLD', N'This is Basic PAckage', 1, CAST(50.0000 AS Money), N'2015-06-18 18:55:15', N'Admin')
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [CarTypeId], [Package_Price], [CreateDt], [CreatedBy]) VALUES (3, N'Complete', N'This is Basic PAckage', 1, CAST(55.9900 AS Money), N'2015-06-18 18:55:35', N'Admin')
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [CarTypeId], [Package_Price], [CreateDt], [CreatedBy]) VALUES (4, N'Basic wash', N'Basic Wash for sedan', 2, CAST(20.0000 AS Money), N'2015-06-28 10:48:31', N'Admin')
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [CarTypeId], [Package_Price], [CreateDt], [CreatedBy]) VALUES (5, N'Basic Wash', N'Basic wash for Small SUV', 3, CAST(20.0000 AS Money), N'2015-06-28 10:48:53', N'Admin')
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [CarTypeId], [Package_Price], [CreateDt], [CreatedBy]) VALUES (6, N'Basic Wash', N'Basic Wash for  SUV', 4, CAST(50.0000 AS Money), N'2015-06-28 10:49:14', N'Admin')
INSERT INTO [dbo].[Packages] ([PackageId], [Package_Name], [Package_Description], [CarTypeId], [Package_Price], [CreateDt], [CreatedBy]) VALUES (7, N'Basic ', N'Basic Wash for Extra Large Vehicles', 5, CAST(55.9900 AS Money), N'2015-06-28 10:49:38', N'Admin')
SET IDENTITY_INSERT [dbo].[Packages] OFF

SET IDENTITY_INSERT [dbo].[Package_Services] ON
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (1, 1, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (2, 1, 2)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (3, 2, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (4, 2, 2)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (5, 2, 3)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (6, 3, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (7, 3, 2)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (8, 3, 3)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (9, 3, 4)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (10, 3, 5)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (11, 4, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (12, 4, 2)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (13, 5, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (14, 5, 2)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (15, 6, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (16, 6, 2)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (17, 7, 1)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (18, 7, 2)
INSERT INTO [dbo].[Package_Services] ([PackageServiceId], [PackageID], [ServiceID]) VALUES (19, 7, 3)
SET IDENTITY_INSERT [dbo].[Package_Services] OFF


INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'aba48f7f-d1e2-4672-a6e1-6de571fb5b64', N'Admin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcba', N'Crew Leader')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9c7e12a2-3726-423f-a408-c2c189b02ac2', N'Users')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcbc', N'Crew Member')


--Users
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'1195b411-51b7-4d71-8ec8-a46aa150504a', N'CrewLeader2@innoator.com', 1, N'ADuzmJ2SWC/JRE4rbyvWqLP5rz1kve9GKCuUZJDcJymU2PKgWFWJNVqosX1wHaIDZg==', N'3202a223-7435-475c-9249-434f7a3c9a68', NULL, 0, 0, NULL, 0, 0, N'CrewLeader2@innoator.com', N'Crew 2', N'Leader 2', N'2012-12-12', N'451 7th Street S.W., Washington, DC 20410', 1, 1, N'412101', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'2fbf30b9-3725-4d35-9493-e56660b12cd9', N'CrewLeader1@innoator.com', 1, N'ACkeyAlIneEcCc39SZ5OlgGoiLZT17dJOwgrKgJoqyXAz5i6LSWcuHrJcteACZC62g==', N'47d7f72d-43df-4133-aad8-a476f4f414d1', NULL, 0, 0, NULL, 0, 0, N'CrewLeader1@innoator.com', N'Crew', N'Leader 1', N'2012-12-12', N'451 7th Street S.W., Washington, DC 20410', 1, 1, N'412101', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'5bd22f33-9887-4f00-accc-64658edabc0f', N'admin@greenpro.com', 1, N'AHjwD5ceY39YQyLd/wI8Yvn7m2ebw7jl8fXwr0jcoWaXp2S6RN/kNwNfXj7I+IjbfA==', N'f7720742-ace0-48af-833b-469a87a9bfae', NULL, 0, 0, NULL, 0, 0, N'admin@greenpro.com', N'Admin', N'GreenPro', N'1960-01-01', N'dsfs', 1, 1, N'1212222', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'7caf7d92-b638-4026-8730-f7f319596bd0', N'Member1@greenpro.com', 1, N'AG/5pFrdJfw5lSWsnTViVsRG8M0k7VDB/ej56kcmD4gpKunqh5JCwgnmg/PAdMd+sA==', N'c17a9e64-8188-412b-a6c6-7e4076cd700f', NULL, 0, 0, NULL, 0, 0, N'Member1@greenpro.com', N'Crew 1', N'Member 1', N'2012-12-12', N'451 7th Street S.W., Washington, DC 20410', 1, 1, N'412101', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'823b511f-4713-49df-b897-89e90f514f2d', N'Member4@greenpro.com', 1, N'AGW1A+KrcWxGvCOrZFWIMNsO0PSL5Yrpoy5iQ1SfuXdz4Lzxo4+SzQaXYC0awEIVGQ==', N'4f2fb6f1-7fbc-4289-a9d4-2a6a1e7ae9b1', NULL, 0, 0, NULL, 0, 0, N'Member4@greenpro.com', N'Crew 4', N'Member 4', N'2012-12-12', N'451 7th Street S.W., Washington, DC 20410', 1, 1, N'412101', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'user2@greenpro.com', 1, N'AOPEILNH8ZYf/Itxj/0PnrcavFMoe78J2KOuLGdcCYcfF8YGcdq0g0M/aXRq3SXvEw==', N'1fc89c3a-b9a8-4467-bab2-92ea8808ceef', NULL, 0, 0, NULL, 0, 0, N'user2@greenpro.com', N'User 2', N'Greenpro', N'1989-01-20', N'Address', 2, 39, N'412101', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'92df0f69-fcbe-434c-a848-4df405004e89', N'user@greenpro.com', 1, N'AMmpRWfdFhqPJmZpUqr/hjLlr4AoZheAep+whumwQ5oP9YdE80eWG3EbKH3WhKj5xg==', N'30ad84bd-7bb5-4fb9-a7c5-099e209306ae', NULL, 0, 0, NULL, 0, 0, N'user@greenpro.com', N'User', N'GreenPRo', N'1960-01-01', N'3909 Witmer Road.
Niagara Falls, NY 14305', 2, 22, N'123456', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'b615b8d9-ae49-4784-8010-8c39eda845ad', N'CrewLeader4@innoator.com', 1, N'AANSwgarO8VxaM4QHQcHIHJIY7JvCZsSSgrfT/Wxey3qjtvWCfQT0bIMVsCqN9IUHg==', N'ed51d416-41fd-4025-b543-ddc8fed27899', NULL, 0, 0, NULL, 0, 0, N'CrewLeader4@innoator.com', N'Crew 3', N'Leader 3', N'2012-12-12', N'451 7th Street S.W., Washington, DC 20410', 1, 1, N'412101', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'bcb56265-dbaf-40f7-9b55-77fc437d7e4f', N'Member3@greenpro.com', 1, N'AMIRC3WdtXcwf6V5nODHerAXN3RTi0g/zqGbnvRo379gkAm1LMqoCfxKIajpwLa9UQ==', N'7789a1fd-c645-4164-a200-5499ab8dcae9', NULL, 0, 0, NULL, 0, 0, N'Member3@greenpro.com', N'Crew 3', N'Member 3', N'2012-12-12', N'451 7th Street S.W., Washington, DC 20410', 1, 1, N'412101', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'c3be3382-df36-49ab-9266-74052562df80', N'CrewLeader3@innoator.com', 1, N'AFlS4C4XsU5HP+8oalkTxq+VlSPlXrSdjbat5Fr7F6qyacqx0WEO0cu5iFthJrlYkg==', N'3aa80596-596c-4861-ad0c-67b4c83572d4', NULL, 0, 0, NULL, 0, 0, N'CrewLeader3@innoator.com', N'Crew 3', N'Leader 3', N'2012-12-12', N'451 7th Street S.W., Washington, DC 20410', 1, 1, N'412101', 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateofBirth], [Address], [State], [City], [Pincode], [Balance]) VALUES (N'eaffb6a0-55cc-42fa-aeca-cf0ed1d5b09f', N'Member2@greenpro.com', 1, N'AJfgIAm26PENpyxYUnOK5ZI//MTf+vrFjFh8voyhUTjJtkunarDWTkLMbeRel4ZMBQ==', N'9609d69d-f25a-4da5-ad6d-1bfba4f6ca57', NULL, 0, 0, NULL, 0, 0, N'Member2@greenpro.com', N'Crew 2', N'Member 2', N'2012-12-12', N'451 7th Street S.W., Washington, DC 20410', 1, 1, N'412101', 0)


--User to Role
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1195b411-51b7-4d71-8ec8-a46aa150504a', N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcba')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2fbf30b9-3725-4d35-9493-e56660b12cd9', N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcba')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b615b8d9-ae49-4784-8010-8c39eda845ad', N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcba')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c3be3382-df36-49ab-9266-74052562df80', N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcba')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7caf7d92-b638-4026-8730-f7f319596bd0', N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcbc')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'823b511f-4713-49df-b897-89e90f514f2d', N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcbc')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bcb56265-dbaf-40f7-9b55-77fc437d7e4f', N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcbc')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'eaffb6a0-55cc-42fa-aeca-cf0ed1d5b09f', N'75c90dfb-5324-4e77-b09d-ee3a4aa0fcbc')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'9c7e12a2-3726-423f-a408-c2c189b02ac2')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'92df0f69-fcbe-434c-a848-4df405004e89', N'9c7e12a2-3726-423f-a408-c2c189b02ac2')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5bd22f33-9887-4f00-accc-64658edabc0f', N'aba48f7f-d1e2-4672-a6e1-6de571fb5b64')

--Add Cars
SET IDENTITY_INSERT [dbo].[CarUsers] ON
INSERT INTO [dbo].[CarUsers] ([CarId], [DisplayName], [Make], [LicenseNumber], [Color], [Type], [PurchaseYear], [UserId], [AutoRenewal], [IsDeleted], [GarageId]) VALUES (1, N'User2Car', N'Mercedes', N'12345678', N'Black', 2, 2015, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', 1, 0, 2)
INSERT INTO [dbo].[CarUsers] ([CarId], [DisplayName], [Make], [LicenseNumber], [Color], [Type], [PurchaseYear], [UserId], [AutoRenewal], [IsDeleted], [GarageId]) VALUES (2, N'User2Car1', N'Hyundai', N'12345678', N'Black', 2, 20015, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', 1, 0, 1)
INSERT INTO [dbo].[CarUsers] ([CarId], [DisplayName], [Make], [LicenseNumber], [Color], [Type], [PurchaseYear], [UserId], [AutoRenewal], [IsDeleted], [GarageId]) VALUES (3, N'User 1 Car', N'Hyundai', N'12345678', N'Black', 2, 2015, N'92df0f69-fcbe-434c-a848-4df405004e89', 1, 0, 1)
INSERT INTO [dbo].[CarUsers] ([CarId], [DisplayName], [Make], [LicenseNumber], [Color], [Type], [PurchaseYear], [UserId], [AutoRenewal], [IsDeleted], [GarageId]) VALUES (4, N'User 1 Car 2', N'Hyundai', N'12345678', N'Black', 1, 20015, N'92df0f69-fcbe-434c-a848-4df405004e89', 1, 0, 2)
SET IDENTITY_INSERT [dbo].[CarUsers] OFF

--User Packages
SET IDENTITY_INSERT [dbo].[UserPackages] ON
INSERT INTO [dbo].[UserPackages] ([Id], [UserId], [PackageId], [CarId], [SubscribedDate], [ActualPrice], [TotalPrice], [PriceWithAddOns], [DiscountPrice], [Ipaddress], [CreatedDt], [PaymentRecieved], [Processed], [TaxAmount], [TipAmount], [SubscriptionEndDate]) VALUES (1, N'92df0f69-fcbe-434c-a848-4df405004e89', 4, 3, N'2015-11-06 11:23:42', CAST(20.0000 AS Money), CAST(25.0000 AS Money), CAST(5.0000 AS Money), CAST(0.0000 AS Money), NULL, N'2015-11-06 11:23:42', 1, 0, CAST(0.0000 AS Money), CAST(0.0000 AS Money), NULL)
INSERT INTO [dbo].[UserPackages] ([Id], [UserId], [PackageId], [CarId], [SubscribedDate], [ActualPrice], [TotalPrice], [PriceWithAddOns], [DiscountPrice], [Ipaddress], [CreatedDt], [PaymentRecieved], [Processed], [TaxAmount], [TipAmount], [SubscriptionEndDate]) VALUES (2, N'92df0f69-fcbe-434c-a848-4df405004e89', 1, 4, N'2015-11-06 11:24:29', CAST(12.0000 AS Money), CAST(39.0000 AS Money), CAST(27.0000 AS Money), CAST(0.0000 AS Money), NULL, N'2015-11-06 11:24:29', 1, 0, CAST(0.0000 AS Money), CAST(0.0000 AS Money), NULL)
INSERT INTO [dbo].[UserPackages] ([Id], [UserId], [PackageId], [CarId], [SubscribedDate], [ActualPrice], [TotalPrice], [PriceWithAddOns], [DiscountPrice], [Ipaddress], [CreatedDt], [PaymentRecieved], [Processed], [TaxAmount], [TipAmount], [SubscriptionEndDate]) VALUES (3, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', 4, 2, N'2015-11-06 11:26:10', CAST(20.0000 AS Money), CAST(40.0000 AS Money), CAST(20.0000 AS Money), CAST(0.0000 AS Money), NULL, N'2015-11-06 11:26:10', 0, 0, CAST(0.0000 AS Money), CAST(0.0000 AS Money), NULL)
INSERT INTO [dbo].[UserPackages] ([Id], [UserId], [PackageId], [CarId], [SubscribedDate], [ActualPrice], [TotalPrice], [PriceWithAddOns], [DiscountPrice], [Ipaddress], [CreatedDt], [PaymentRecieved], [Processed], [TaxAmount], [TipAmount], [SubscriptionEndDate]) VALUES (4, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', 4, 2, N'2015-11-06 11:27:15', CAST(20.0000 AS Money), CAST(52.0000 AS Money), CAST(32.0000 AS Money), CAST(0.0000 AS Money), NULL, N'2015-11-06 11:27:15', 1, 0, CAST(0.0000 AS Money), CAST(0.0000 AS Money), NULL)
INSERT INTO [dbo].[UserPackages] ([Id], [UserId], [PackageId], [CarId], [SubscribedDate], [ActualPrice], [TotalPrice], [PriceWithAddOns], [DiscountPrice], [Ipaddress], [CreatedDt], [PaymentRecieved], [Processed], [TaxAmount], [TipAmount], [SubscriptionEndDate]) VALUES (5, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', 4, 1, N'2015-11-06 11:54:18', CAST(20.0000 AS Money), CAST(40.0000 AS Money), CAST(20.0000 AS Money), CAST(0.0000 AS Money), NULL, N'2015-11-06 11:54:18', 1, 0, CAST(0.0000 AS Money), CAST(0.0000 AS Money), NULL)
SET IDENTITY_INSERT [dbo].[UserPackages] OFF

--package addons
SET IDENTITY_INSERT [dbo].[UserPackagesAddons] ON
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (1, 1, 3, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:23:42')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (2, 2, 4, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:24:29')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (3, 2, 5, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:24:29')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (4, 3, 3, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:26:10')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (5, 3, 4, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:26:10')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (6, 4, 3, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:27:15')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (7, 4, 4, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:27:15')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (8, 4, 5, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:27:15')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (9, 5, 3, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:54:18')
INSERT INTO [dbo].[UserPackagesAddons] ([Id], [UserPackageID], [ServiceID], [ActualPrice], [DiscountPrice], [CreatedDt]) VALUES (10, 5, 4, CAST(0.0000 AS Money), CAST(0.0000 AS Money), N'2015-11-06 11:54:18')
SET IDENTITY_INSERT [dbo].[UserPackagesAddons] OFF


--UserTrasactions
SET IDENTITY_INSERT [dbo].[UserTransactions] ON
INSERT INTO [dbo].[UserTransactions] ([Id], [TransactionDate], [Amount], [PackageId], [PaypalId], [Details], [Userid]) VALUES (1, N'2015-11-06 11:24:08', CAST(25.00 AS Decimal(18, 2)), 1, N'EC-4LW524694L0948156', N'No Details', N'92df0f69-fcbe-434c-a848-4df405004e89')
INSERT INTO [dbo].[UserTransactions] ([Id], [TransactionDate], [Amount], [PackageId], [PaypalId], [Details], [Userid]) VALUES (2, N'2015-11-06 11:24:44', CAST(39.00 AS Decimal(18, 2)), 2, N'EC-5H069604W38319210', N'No Details', N'92df0f69-fcbe-434c-a848-4df405004e89')
INSERT INTO [dbo].[UserTransactions] ([Id], [TransactionDate], [Amount], [PackageId], [PaypalId], [Details], [Userid]) VALUES (3, N'2015-11-06 11:52:31', CAST(52.00 AS Decimal(18, 2)), 4, N'EC-0LY8398091984943X', N'No Details', N'8e6cb4eb-5714-46b9-af95-44b0105b0443')
INSERT INTO [dbo].[UserTransactions] ([Id], [TransactionDate], [Amount], [PackageId], [PaypalId], [Details], [Userid]) VALUES (4, N'2015-11-06 11:54:36', CAST(40.00 AS Decimal(18, 2)), 5, N'EC-1K065294WR025760T', N'No Details', N'8e6cb4eb-5714-46b9-af95-44b0105b0443')
SET IDENTITY_INSERT [dbo].[UserTransactions] OFF

--PaypalTrasaction
SET IDENTITY_INSERT [dbo].[PayPalLogs] ON
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (1, N'92df0f69-fcbe-434c-a848-4df405004e89', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-4LW524694L0948156', N'EC-4LW524694L0948156', N'', N'2015-11-06T05:53:48Z', N'dc84f5eee1808', N'Express', N'2015-11-06 11:23:47', 1)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (2, N'92df0f69-fcbe-434c-a848-4df405004e89', N'SUCCESS', N'', N'', N'EC-4LW524694L0948156', N'B-8HJ06611ST638252W', N'2015-11-06T05:54:09Z', N'81bdc610edd37', N'BillingAgreement', N'2015-11-06 11:24:08', 1)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (3, N'92df0f69-fcbe-434c-a848-4df405004e89', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-5H069604W38319210', N'EC-5H069604W38319210', N'', N'2015-11-06T05:54:33Z', N'56ad9200a35b8', N'Express', N'2015-11-06 11:24:33', 2)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (4, N'92df0f69-fcbe-434c-a848-4df405004e89', N'SUCCESS', N'', N'', N'EC-5H069604W38319210', N'B-40586153X3627821C', N'2015-11-06T05:54:45Z', N'bb8c6ed5334d', N'BillingAgreement', N'2015-11-06 11:24:44', 2)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (5, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-0HM52522CA402173E', N'EC-0HM52522CA402173E', N'', N'2015-11-06T05:56:16Z', N'e50fbc01d4909', N'Express', N'2015-11-06 11:26:15', 3)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (6, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-72J49952DR137774U', N'EC-72J49952DR137774U', N'', N'2015-11-06T05:57:21Z', N'22ddca96e3ec1', N'Express', N'2015-11-06 11:27:20', 4)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (7, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-0LY8398091984943X', N'EC-0LY8398091984943X', N'', N'2015-11-06T06:22:18Z', N'1d68c4ed897b1', N'Express', N'2015-11-06 11:52:17', 4)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (8, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'', N'EC-0LY8398091984943X', N'B-5WV621495V257831T', N'2015-11-06T06:22:32Z', N'1542150ee96f0', N'BillingAgreement', N'2015-11-06 11:52:31', 4)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (9, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-1K065294WR025760T', N'EC-1K065294WR025760T', N'', N'2015-11-06T06:24:24Z', N'a324108c7ec8b', N'Express', N'2015-11-06 11:54:23', 5)
INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (10, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'', N'EC-1K065294WR025760T', N'B-46L15267H8816643L', N'2015-11-06T06:24:37Z', N'64d2d81410367', N'BillingAgreement', N'2015-11-06 11:54:36', 5)
SET IDENTITY_INSERT [dbo].[PayPalLogs] OFF

---leader/member garages
SET IDENTITY_INSERT [dbo].[WorkerGarages] ON
INSERT INTO [dbo].[WorkerGarages] ([Id], [CrewLeaderId], [GarageID], [IsLeader]) VALUES (1, N'1195b411-51b7-4d71-8ec8-a46aa150504a', 1, 1)
INSERT INTO [dbo].[WorkerGarages] ([Id], [CrewLeaderId], [GarageID], [IsLeader]) VALUES (2, N'2fbf30b9-3725-4d35-9493-e56660b12cd9', 1, 1)
INSERT INTO [dbo].[WorkerGarages] ([Id], [CrewLeaderId], [GarageID], [IsLeader]) VALUES (3, N'b615b8d9-ae49-4784-8010-8c39eda845ad', 2, 1)
INSERT INTO [dbo].[WorkerGarages] ([Id], [CrewLeaderId], [GarageID], [IsLeader]) VALUES (4, N'c3be3382-df36-49ab-9266-74052562df80', 2, 1)
INSERT INTO [dbo].[WorkerGarages] ([Id], [CrewLeaderId], [GarageID], [IsLeader]) VALUES (5, N'7caf7d92-b638-4026-8730-f7f319596bd0', 1, 0)
INSERT INTO [dbo].[WorkerGarages] ([Id], [CrewLeaderId], [GarageID], [IsLeader]) VALUES (6, N'823b511f-4713-49df-b897-89e90f514f2d', 1, 0)
INSERT INTO [dbo].[WorkerGarages] ([Id], [CrewLeaderId], [GarageID], [IsLeader]) VALUES (7, N'bcb56265-dbaf-40f7-9b55-77fc437d7e4f', 2, 0)
INSERT INTO [dbo].[WorkerGarages] ([Id], [CrewLeaderId], [GarageID], [IsLeader]) VALUES (8, N'eaffb6a0-55cc-42fa-aeca-cf0ed1d5b09f', 2, 0)
SET IDENTITY_INSERT [dbo].[WorkerGarages] OFF




--SET IDENTITY_INSERT [dbo].[PayPalLogs] ON
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (1, N'92df0f69-fcbe-434c-a848-4df405004e89', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-4LW524694L0948156', N'EC-4LW524694L0948156', N'', N'2015-11-06T05:53:48Z', N'dc84f5eee1808', N'Express', N'2015-11-06 11:23:47', 1)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (2, N'92df0f69-fcbe-434c-a848-4df405004e89', N'SUCCESS', N'', N'', N'EC-4LW524694L0948156', N'B-8HJ06611ST638252W', N'2015-11-06T05:54:09Z', N'81bdc610edd37', N'BillingAgreement', N'2015-11-06 11:24:08', 1)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (3, N'92df0f69-fcbe-434c-a848-4df405004e89', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-5H069604W38319210', N'EC-5H069604W38319210', N'', N'2015-11-06T05:54:33Z', N'56ad9200a35b8', N'Express', N'2015-11-06 11:24:33', 2)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (4, N'92df0f69-fcbe-434c-a848-4df405004e89', N'SUCCESS', N'', N'', N'EC-5H069604W38319210', N'B-40586153X3627821C', N'2015-11-06T05:54:45Z', N'bb8c6ed5334d', N'BillingAgreement', N'2015-11-06 11:24:44', 2)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (5, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-0HM52522CA402173E', N'EC-0HM52522CA402173E', N'', N'2015-11-06T05:56:16Z', N'e50fbc01d4909', N'Express', N'2015-11-06 11:26:15', 3)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (6, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-72J49952DR137774U', N'EC-72J49952DR137774U', N'', N'2015-11-06T05:57:21Z', N'22ddca96e3ec1', N'Express', N'2015-11-06 11:27:20', 4)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (7, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-0LY8398091984943X', N'EC-0LY8398091984943X', N'', N'2015-11-06T06:22:18Z', N'1d68c4ed897b1', N'Express', N'2015-11-06 11:52:17', 4)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (8, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'', N'EC-0LY8398091984943X', N'B-5WV621495V257831T', N'2015-11-06T06:22:32Z', N'1542150ee96f0', N'BillingAgreement', N'2015-11-06 11:52:31', 4)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (9, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=EC-1K065294WR025760T', N'EC-1K065294WR025760T', N'', N'2015-11-06T06:24:24Z', N'a324108c7ec8b', N'Express', N'2015-11-06 11:54:23', 5)
--INSERT INTO [dbo].[PayPalLogs] ([LogId], [UserId], [ApiSatus], [ResponseError], [ResponseRedirectURL], [ECToken], [BillingAggrementID], [TimeStamp], [CorrelationID], [ACK], [ServerDate], [SubscriptionID]) VALUES (10, N'8e6cb4eb-5714-46b9-af95-44b0105b0443', N'SUCCESS', N'', N'', N'EC-1K065294WR025760T', N'B-46L15267H8816643L', N'2015-11-06T06:24:37Z', N'64d2d81410367', N'BillingAgreement', N'2015-11-06 11:54:36', 5)
--SET IDENTITY_INSERT [dbo].[PayPalLogs] OFF