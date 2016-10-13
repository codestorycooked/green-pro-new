CREATE TABLE [dbo].[WorkLogDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[SideId] [int] NOT NULL,
	[PrePost] [nvarchar](50) NOT NULL,
	[ImageRef] [nvarchar](max) NOT NULL,
	[WorkDoneId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_WorkLogDetails_LogDetailCarSide]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkLogDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkLogDetails_LogDetailCarSide] FOREIGN KEY([SideId])
REFERENCES [dbo].[LogDetailCarSide] ([Id])
GO

ALTER TABLE [dbo].[WorkLogDetails] CHECK CONSTRAINT [FK_WorkLogDetails_LogDetailCarSide]
GO
/****** Object:  ForeignKey [FK_WorkLogDetails_WorkDone]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[WorkLogDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkLogDetails_WorkDone] FOREIGN KEY([WorkDoneId])
REFERENCES [dbo].[WorkDone] ([Id])
GO

ALTER TABLE [dbo].[WorkLogDetails] CHECK CONSTRAINT [FK_WorkLogDetails_WorkDone]