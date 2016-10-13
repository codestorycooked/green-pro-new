CREATE TABLE [dbo].[PaypalAutoPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrasactionID] [nvarchar](150) NULL,
	[ReferenceID] [nvarchar](150) NULL,
	[PaymentStatus] [nvarchar](50) NULL,
	[PendingReason] [nvarchar](150) NULL,
	[PaymentDate] [nvarchar](150) NULL,
	[GrossAmount] [nvarchar](150) NULL,
	[UserPackageID] [int] NULL,
	[UserID] [nvarchar](128) NULL,
	[TransactionDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[ServiceDate] [date] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[BillingAggrementID] [nvarchar](100) NULL,
 CONSTRAINT [PK__PaypalAu__3214EC075165187F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_PaypalAutoPayments_AspNEtUSERs]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[PaypalAutoPayments]  WITH CHECK ADD  CONSTRAINT [FK_PaypalAutoPayments_AspNEtUSERs] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[PaypalAutoPayments] CHECK CONSTRAINT [FK_PaypalAutoPayments_AspNEtUSERs]
GO
/****** Object:  ForeignKey [FK_PaypalAutoPayments_UserPackageID]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[PaypalAutoPayments]  WITH CHECK ADD  CONSTRAINT [FK_PaypalAutoPayments_UserPackageID] FOREIGN KEY([UserPackageID])
REFERENCES [dbo].[UserPackages] ([Id])
GO

ALTER TABLE [dbo].[PaypalAutoPayments] CHECK CONSTRAINT [FK_PaypalAutoPayments_UserPackageID]