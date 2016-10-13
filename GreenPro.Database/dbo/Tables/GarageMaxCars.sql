CREATE TABLE [dbo].[GarageMaxCars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GarageID] [int] NOT NULL,
	[CarPerCrewAdmin] [int] NOT NULL,
	[DayRef] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_GarageMaxCars_Garages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[GarageMaxCars]  WITH CHECK ADD  CONSTRAINT [FK_GarageMaxCars_Garages] FOREIGN KEY([GarageID])
REFERENCES [dbo].[Garages] ([GarageId])
GO

ALTER TABLE [dbo].[GarageMaxCars] CHECK CONSTRAINT [FK_GarageMaxCars_Garages]