CREATE TABLE [dbo].[CarUsers](
	[CarId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
	[Make] [nvarchar](50) NOT NULL,
	[LicenseNumber] [nvarchar](50) NOT NULL,
	[Color] [nvarchar](50) NULL,
	[PurchaseYear] [int] NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[AutoRenewal] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[GarageId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CarId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_CarUsers_Garage]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CarUsers]  WITH CHECK ADD  CONSTRAINT [FK_CarUsers_Garage] FOREIGN KEY([GarageId])
REFERENCES [dbo].[Garages] ([GarageId])
GO

ALTER TABLE [dbo].[CarUsers] CHECK CONSTRAINT [FK_CarUsers_Garage]
GO
/****** Object:  ForeignKey [FK_CarUsers_Users]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CarUsers]  WITH CHECK ADD  CONSTRAINT [FK_CarUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[CarUsers] CHECK CONSTRAINT [FK_CarUsers_Users]
GO
/****** Object:  Default [DF__CarUsers__AutoRe__05D8E0BE]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CarUsers] ADD  DEFAULT ((0)) FOR [AutoRenewal]
GO
/****** Object:  Default [DF__CarUsers__IsDele__06CD04F7]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CarUsers] ADD  DEFAULT ((0)) FOR [IsDeleted]