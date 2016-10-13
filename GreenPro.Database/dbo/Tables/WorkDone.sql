CREATE TABLE [dbo].[WorkDone](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CrewLeaderID] [nvarchar](128) NOT NULL,
	[CrewMemberId] [nvarchar](128) NOT NULL,
	[PackageId] [int] NOT NULL,
	[StartTimeStamp] [datetime] NULL,
	[EndTimeStamp] [datetime] NULL,
	[IsCompleted] [bit] NOT NULL,
	[CreatedTimeStamp] [datetime] NULL,
	[UserId] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_WorkDone_AspNetUsers1]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkDone]  WITH CHECK ADD  CONSTRAINT [FK_WorkDone_AspNetUsers1] FOREIGN KEY([CrewLeaderID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[WorkDone] CHECK CONSTRAINT [FK_WorkDone_AspNetUsers1]
GO
/****** Object:  ForeignKey [FK_WorkDone_AspNetUsers2]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkDone]  WITH CHECK ADD  CONSTRAINT [FK_WorkDone_AspNetUsers2] FOREIGN KEY([CrewMemberId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[WorkDone] CHECK CONSTRAINT [FK_WorkDone_AspNetUsers2]
GO
/****** Object:  ForeignKey [FK_WorkDone_Packages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkDone]  WITH CHECK ADD  CONSTRAINT [FK_WorkDone_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO

ALTER TABLE [dbo].[WorkDone] CHECK CONSTRAINT [FK_WorkDone_Packages]
GO
/****** Object:  Default [DF__WorkDone__IsComp__0B91BA14]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkDone] ADD  DEFAULT ((0)) FOR [IsCompleted]