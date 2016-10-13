CREATE TABLE [dbo].[UnAssignedCars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GarageID] [int] NOT NULL,
	[PackageId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_UnAssignedCars_Garages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UnAssignedCars]  WITH CHECK ADD  CONSTRAINT [FK_UnAssignedCars_Garages] FOREIGN KEY([GarageID])
REFERENCES [dbo].[Garages] ([GarageId])
GO

ALTER TABLE [dbo].[UnAssignedCars] CHECK CONSTRAINT [FK_UnAssignedCars_Garages]
GO
/****** Object:  ForeignKey [FK_UnAssignedCars_Packages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UnAssignedCars]  WITH CHECK ADD  CONSTRAINT [FK_UnAssignedCars_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[UserPackages] ([Id])
GO

ALTER TABLE [dbo].[UnAssignedCars] CHECK CONSTRAINT [FK_UnAssignedCars_Packages]