CREATE TABLE [dbo].[UserPackagesAddons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserPackageID] [int] NOT NULL,
	[ServiceID] [int] NOT NULL,
	[ActualPrice] [money] NOT NULL,
	[DiscountPrice] [money] NOT NULL,
	[CreatedDt] [datetime] NULL,
	[NextServiceDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_UserPackagesAddons_UserPackages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackagesAddons]  WITH CHECK ADD  CONSTRAINT [FK_UserPackagesAddons_UserPackages] FOREIGN KEY([UserPackageID])
REFERENCES [dbo].[UserPackages] ([Id])
GO

ALTER TABLE [dbo].[UserPackagesAddons] CHECK CONSTRAINT [FK_UserPackagesAddons_UserPackages]