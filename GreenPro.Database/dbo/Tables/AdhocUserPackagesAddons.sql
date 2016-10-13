CREATE TABLE [dbo].[AdhocUserPackagesAddons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserPackageID] [int] NOT NULL,
	[ServiceID] [int] NOT NULL,
	[ActualPrice] [money] NOT NULL,
	[DiscountPrice] [money] NOT NULL,
	[CreatedDt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_AdhocUserPackagesAddons_AdhocUserPackages]    Script Date: 10/13/2016 00:01:22 ******/
ALTER TABLE [dbo].[AdhocUserPackagesAddons]  WITH CHECK ADD  CONSTRAINT [FK_AdhocUserPackagesAddons_AdhocUserPackages] FOREIGN KEY([UserPackageID])
REFERENCES [dbo].[AdhocUserPackages] ([Id])
GO

ALTER TABLE [dbo].[AdhocUserPackagesAddons] CHECK CONSTRAINT [FK_AdhocUserPackagesAddons_AdhocUserPackages]
GO
/****** Object:  ForeignKey [FK_AdhocUserPackagesAddons_ToServices]    Script Date: 10/13/2016 00:01:22 ******/
ALTER TABLE [dbo].[AdhocUserPackagesAddons]  WITH CHECK ADD  CONSTRAINT [FK_AdhocUserPackagesAddons_ToServices] FOREIGN KEY([ServiceID])
REFERENCES [dbo].[Services] ([ServiceID])
GO

ALTER TABLE [dbo].[AdhocUserPackagesAddons] CHECK CONSTRAINT [FK_AdhocUserPackagesAddons_ToServices]