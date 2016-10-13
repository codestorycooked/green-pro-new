CREATE TABLE [dbo].[WorkerGarages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CrewLeaderId] [nvarchar](128) NOT NULL,
	[GarageID] [int] NOT NULL,
	[IsLeader] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_WorkerGarages_AspNetUser]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkerGarages]  WITH CHECK ADD  CONSTRAINT [FK_WorkerGarages_AspNetUser] FOREIGN KEY([CrewLeaderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[WorkerGarages] CHECK CONSTRAINT [FK_WorkerGarages_AspNetUser]
GO
/****** Object:  ForeignKey [FK_WorkerGarages_Garages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkerGarages]  WITH CHECK ADD  CONSTRAINT [FK_WorkerGarages_Garages] FOREIGN KEY([GarageID])
REFERENCES [dbo].[Garages] ([GarageId])
GO

ALTER TABLE [dbo].[WorkerGarages] CHECK CONSTRAINT [FK_WorkerGarages_Garages]
GO
/****** Object:  Default [DF__WorkerGar__IsLea__0C85DE4D]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkerGarages] ADD  DEFAULT ((0)) FOR [IsLeader]