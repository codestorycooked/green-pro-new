CREATE TABLE [dbo].[Package_Services](
	[PackageServiceId] [int] IDENTITY(1,1) NOT NULL,
	[PackageID] [int] NOT NULL,
	[ServiceID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PackageServiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Package_Services_Packages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[Package_Services]  WITH CHECK ADD  CONSTRAINT [FK_Package_Services_Packages] FOREIGN KEY([PackageID])
REFERENCES [dbo].[Packages] ([PackageId])
GO

ALTER TABLE [dbo].[Package_Services] CHECK CONSTRAINT [FK_Package_Services_Packages]
GO
/****** Object:  ForeignKey [FK_Package_Services_Services]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[Package_Services]  WITH CHECK ADD  CONSTRAINT [FK_Package_Services_Services] FOREIGN KEY([ServiceID])
REFERENCES [dbo].[Services] ([ServiceID])
GO

ALTER TABLE [dbo].[Package_Services] CHECK CONSTRAINT [FK_Package_Services_Services]