CREATE TABLE [dbo].[PayPalLogs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[ApiSatus] [nvarchar](50) NULL,
	[ResponseError] [nvarchar](256) NULL,
	[ResponseRedirectURL] [nvarchar](100) NULL,
	[ECToken] [nvarchar](100) NULL,
	[BillingAggrementID] [nvarchar](100) NULL,
	[TimeStamp] [varchar](50) NULL,
	[CorrelationID] [varchar](50) NULL,
	[ACK] [varchar](50) NULL,
	[ServerDate] [datetime] NULL,
	[SubscriptionID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_PayPalLogs_UserPackage]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[PayPalLogs]  WITH CHECK ADD  CONSTRAINT [FK_PayPalLogs_UserPackage] FOREIGN KEY([SubscriptionID])
REFERENCES [dbo].[UserPackages] ([Id])
GO

ALTER TABLE [dbo].[PayPalLogs] CHECK CONSTRAINT [FK_PayPalLogs_UserPackage]