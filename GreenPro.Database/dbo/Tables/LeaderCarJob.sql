CREATE TABLE [dbo].[LeaderCarJob](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Day] [nvarchar](50) NOT NULL,
	[GarageId] [int] NOT NULL,
	[LeaderId] [nvarchar](128) NOT NULL,
	[CarId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_LeaderCarJob_AspNetUser]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[LeaderCarJob]  WITH CHECK ADD  CONSTRAINT [FK_LeaderCarJob_AspNetUser] FOREIGN KEY([LeaderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[LeaderCarJob] CHECK CONSTRAINT [FK_LeaderCarJob_AspNetUser]
GO
/****** Object:  ForeignKey [FK_LeaderCarJob_CarUser]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[LeaderCarJob]  WITH CHECK ADD  CONSTRAINT [FK_LeaderCarJob_CarUser] FOREIGN KEY([CarId])
REFERENCES [dbo].[CarUsers] ([CarId])
GO

ALTER TABLE [dbo].[LeaderCarJob] CHECK CONSTRAINT [FK_LeaderCarJob_CarUser]
GO
/****** Object:  ForeignKey [FK_LeaderCarJob_Garages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[LeaderCarJob]  WITH CHECK ADD  CONSTRAINT [FK_LeaderCarJob_Garages] FOREIGN KEY([GarageId])
REFERENCES [dbo].[Garages] ([GarageId])
GO

ALTER TABLE [dbo].[LeaderCarJob] CHECK CONSTRAINT [FK_LeaderCarJob_Garages]