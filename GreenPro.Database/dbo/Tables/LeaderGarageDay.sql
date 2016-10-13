CREATE TABLE [dbo].[LeaderGarageDay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkerGarageID] [int] NOT NULL,
	[Day] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_LeaderGarageDay_WorkerGarages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[LeaderGarageDay]  WITH CHECK ADD  CONSTRAINT [FK_LeaderGarageDay_WorkerGarages] FOREIGN KEY([WorkerGarageID])
REFERENCES [dbo].[WorkerGarages] ([Id])
GO

ALTER TABLE [dbo].[LeaderGarageDay] CHECK CONSTRAINT [FK_LeaderGarageDay_WorkerGarages]