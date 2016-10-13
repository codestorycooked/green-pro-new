CREATE TABLE [dbo].[UserPackages](
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
	[PaymentRecieved] [bit] NOT NULL,
	[Processed] [bit] NOT NULL,
	[TaxAmount] [money] NOT NULL,
	[TipAmount] [money] NOT NULL,
	[SubscriptionEndDate] [datetime] NULL,
	[ServiceDay] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[GaragesTimeingSlotId] [int] NULL,
	[NextServiceDate] [date] NULL,
	[LastServiceDate] [date] NULL,
	[SubscriptionTypeId] [int] NOT NULL,
	[PaymentMethodName] [nvarchar](50) NULL,
 CONSTRAINT [PK__UserPack__3214EC0768487DD7] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_UserPackages_CarUsers]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackages]  WITH CHECK ADD  CONSTRAINT [FK_UserPackages_CarUsers] FOREIGN KEY([CarId])
REFERENCES [dbo].[CarUsers] ([CarId])
GO

ALTER TABLE [dbo].[UserPackages] CHECK CONSTRAINT [FK_UserPackages_CarUsers]
GO
/****** Object:  ForeignKey [FK_UserPackages_Packages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackages]  WITH CHECK ADD  CONSTRAINT [FK_UserPackages_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO

ALTER TABLE [dbo].[UserPackages] CHECK CONSTRAINT [FK_UserPackages_Packages]
GO
/****** Object:  ForeignKey [FK_UserPackages_Users]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackages]  WITH CHECK ADD  CONSTRAINT [FK_UserPackages_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[UserPackages] CHECK CONSTRAINT [FK_UserPackages_Users]
GO
/****** Object:  Default [DF__UserPacka__Payme__07C12930]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackages] ADD  CONSTRAINT [DF__UserPacka__Payme__07C12930]  DEFAULT ((0)) FOR [PaymentRecieved]
GO
/****** Object:  Default [DF__UserPacka__Proce__08B54D69]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackages] ADD  CONSTRAINT [DF__UserPacka__Proce__08B54D69]  DEFAULT ((0)) FOR [Processed]
GO
/****** Object:  Default [DF__UserPacka__TaxAm__09A971A2]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackages] ADD  CONSTRAINT [DF__UserPacka__TaxAm__09A971A2]  DEFAULT ((0)) FOR [TaxAmount]
GO
/****** Object:  Default [DF__UserPacka__TipAm__0A9D95DB]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackages] ADD  CONSTRAINT [DF__UserPacka__TipAm__0A9D95DB]  DEFAULT ((0)) FOR [TipAmount]
GO
/****** Object:  Default [DF_UserPackages_SubscriptionTypeId]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserPackages] ADD  CONSTRAINT [DF_UserPackages_SubscriptionTypeId]  DEFAULT ((1)) FOR [SubscriptionTypeId]