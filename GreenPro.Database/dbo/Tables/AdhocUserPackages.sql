CREATE TABLE [dbo].[AdhocUserPackages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[PackageId] [int] NOT NULL,
	[CarId] [int] NOT NULL,
	[SubscribedDate] [datetime] NOT NULL,
	[ActualPrice] [money] NOT NULL,
	[TotalPrice] [money] NOT NULL,
	[PriceWithAddOns] [money] NOT NULL,
	[DiscountPrice] [money] NOT NULL,
	[Ipaddress] [nvarchar](20) NULL,
	[CreatedDt] [datetime] NOT NULL,
	[ExistingPackageId] [int] NOT NULL,
	[PaymentRecieved] [bit] NOT NULL,
	[Processed] [bit] NOT NULL,
	[TaxAmount] [money] NOT NULL,
	[TipAmount] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_AdhocUserPackages_CarUsers]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AdhocUserPackages]  WITH CHECK ADD  CONSTRAINT [FK_AdhocUserPackages_CarUsers] FOREIGN KEY([CarId])
REFERENCES [dbo].[CarUsers] ([CarId])
GO

ALTER TABLE [dbo].[AdhocUserPackages] CHECK CONSTRAINT [FK_AdhocUserPackages_CarUsers]
GO
/****** Object:  ForeignKey [FK_AdhocUserPackages_Packages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AdhocUserPackages]  WITH CHECK ADD  CONSTRAINT [FK_AdhocUserPackages_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO

ALTER TABLE [dbo].[AdhocUserPackages] CHECK CONSTRAINT [FK_AdhocUserPackages_Packages]
GO
/****** Object:  ForeignKey [FK_AdhocUserPackages_UserPackages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AdhocUserPackages]  WITH CHECK ADD  CONSTRAINT [FK_AdhocUserPackages_UserPackages] FOREIGN KEY([ExistingPackageId])
REFERENCES [dbo].[UserPackages] ([Id])
GO

ALTER TABLE [dbo].[AdhocUserPackages] CHECK CONSTRAINT [FK_AdhocUserPackages_UserPackages]
GO
/****** Object:  ForeignKey [FK_AdhocUserPackages_Users]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AdhocUserPackages]  WITH CHECK ADD  CONSTRAINT [FK_AdhocUserPackages_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[AdhocUserPackages] CHECK CONSTRAINT [FK_AdhocUserPackages_Users]
GO
/****** Object:  Default [DF__AdhocUser__Payme__01142BA1]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AdhocUserPackages] ADD  DEFAULT ((0)) FOR [PaymentRecieved]
GO
/****** Object:  Default [DF__AdhocUser__Proce__02084FDA]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AdhocUserPackages] ADD  DEFAULT ((0)) FOR [Processed]
GO
/****** Object:  Default [DF__AdhocUser__TaxAm__02FC7413]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AdhocUserPackages] ADD  DEFAULT ((0)) FOR [TaxAmount]
GO
/****** Object:  Default [DF__AdhocUser__TipAm__03F0984C]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AdhocUserPackages] ADD  DEFAULT ((0)) FOR [TipAmount]