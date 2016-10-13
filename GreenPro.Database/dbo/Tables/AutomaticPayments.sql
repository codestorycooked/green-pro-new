CREATE TABLE [dbo].[AutomaticPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](155) NULL,
	[UserPackageID] [int] NULL,
	[TotalAmount] [money] NULL,
	[Remarks] [nvarchar](255) NULL,
	[TransactionDate] [datetime] NULL,
	[PaypalBillingID] [nvarchar](50) NULL,
	[PayPalECToken] [nvarchar](50) NULL,
	[AdhocUserPackageID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]