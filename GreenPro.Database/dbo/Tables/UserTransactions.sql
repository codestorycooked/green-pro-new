CREATE TABLE [dbo].[UserTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Amount] [numeric](18, 2) NOT NULL,
	[PackageId] [int] NULL,
	[PaypalId] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[Userid] [nvarchar](128) NOT NULL,
	[BillingAggrementID] [nvarchar](500) NULL,
	[TrasactionID] [nvarchar](150) NULL,
	[ReferenceID] [nvarchar](150) NULL,
 CONSTRAINT [PK__UserTran__3214EC076FE99F9F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_UserTransactions_ASpnetUsers]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserTransactions]  WITH CHECK ADD  CONSTRAINT [FK_UserTransactions_ASpnetUsers] FOREIGN KEY([Userid])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[UserTransactions] CHECK CONSTRAINT [FK_UserTransactions_ASpnetUsers]
GO
/****** Object:  ForeignKey [FK_UserTransactions_UserPackages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[UserTransactions]  WITH CHECK ADD  CONSTRAINT [FK_UserTransactions_UserPackages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[UserPackages] ([Id])
GO

ALTER TABLE [dbo].[UserTransactions] CHECK CONSTRAINT [FK_UserTransactions_UserPackages]