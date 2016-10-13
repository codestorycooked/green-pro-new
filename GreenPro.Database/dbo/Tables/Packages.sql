CREATE TABLE [dbo].[Packages](
	[PackageId] [int] IDENTITY(1,1) NOT NULL,
	[Package_Name] [nvarchar](100) NOT NULL,
	[Package_Description] [nvarchar](255) NOT NULL,
	[Package_Price] [money] NOT NULL,
	[CreateDt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[SubscriptionTypes] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]