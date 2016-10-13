CREATE TABLE [dbo].[Garages](
	[GarageId] [int] IDENTITY(1,1) NOT NULL,
	[Garage_Name] [nvarchar](100) NOT NULL,
	[Garage_Address] [nvarchar](255) NOT NULL,
	[State] [int] NOT NULL,
	[City] [int] NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Pincode] [nvarchar](10) NOT NULL,
	[Latitute] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedDt] [date] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[Contact_Person] [nvarchar](100) NOT NULL,
	[Phone_Number] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[OpenTime] [time](7) NULL,
	[CloseTime] [time](7) NOT NULL,
	[ServiceDays] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[GarageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Garages_Cities]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[Garages]  WITH CHECK ADD  CONSTRAINT [FK_Garages_Cities] FOREIGN KEY([City])
REFERENCES [dbo].[Cities] ([Id])
GO

ALTER TABLE [dbo].[Garages] CHECK CONSTRAINT [FK_Garages_Cities]
GO
/****** Object:  ForeignKey [FK_Garages_States]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[Garages]  WITH CHECK ADD  CONSTRAINT [FK_Garages_States] FOREIGN KEY([State])
REFERENCES [dbo].[States] ([Id])
GO

ALTER TABLE [dbo].[Garages] CHECK CONSTRAINT [FK_Garages_States]