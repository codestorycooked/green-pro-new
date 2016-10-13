CREATE TABLE [dbo].[Garage_LeaderSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GarageTeamId] [int] NOT NULL,
	[EntityTypeKey] [int] NOT NULL,
	[EntityTypeValue] [nvarchar](128) NOT NULL,
	[GarageId] [int] NOT NULL,
	[ServiceDay] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Garage_LeaderSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]