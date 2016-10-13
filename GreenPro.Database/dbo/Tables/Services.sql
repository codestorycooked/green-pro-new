CREATE TABLE [dbo].[Services](
	[ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[Service_Name] [nvarchar](100) NOT NULL,
	[Service_Description] [nvarchar](200) NOT NULL,
	[Service_Price] [money] NOT NULL,
	[IsAddOn] [bit] NOT NULL,
	[CreateDt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Services__C51BB0EA59063A47] PRIMARY KEY CLUSTERED 
(
	[ServiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Services_IsAddOn]    Script Date: 10/13/2016 00:01:18 ******/
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF_Services_IsAddOn]  DEFAULT ((0)) FOR [IsAddOn]