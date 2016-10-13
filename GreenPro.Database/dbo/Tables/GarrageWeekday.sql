CREATE TABLE [dbo].[GarrageWeekday](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GarrageId] [int] NOT NULL,
	[WeekdayId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_GarrageWeekday_Garages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[GarrageWeekday]  WITH CHECK ADD  CONSTRAINT [FK_GarrageWeekday_Garages] FOREIGN KEY([GarrageId])
REFERENCES [dbo].[Garages] ([GarageId])
GO

ALTER TABLE [dbo].[GarrageWeekday] CHECK CONSTRAINT [FK_GarrageWeekday_Garages]
GO
/****** Object:  ForeignKey [FK_GarrageWeekday_Weekdays]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[GarrageWeekday]  WITH CHECK ADD  CONSTRAINT [FK_GarrageWeekday_Weekdays] FOREIGN KEY([WeekdayId])
REFERENCES [dbo].[Weekdays] ([Id])
GO

ALTER TABLE [dbo].[GarrageWeekday] CHECK CONSTRAINT [FK_GarrageWeekday_Weekdays]