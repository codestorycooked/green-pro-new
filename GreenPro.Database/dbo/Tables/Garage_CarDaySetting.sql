CREATE TABLE [dbo].[Garage_CarDaySetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GarageTeamId] [int] NOT NULL,
	[EntityTypeKey] [int] NOT NULL,
	[EntityTypeValue] [nvarchar](128) NOT NULL,
	[GarageId] [int] NOT NULL,
	[ServiceDay] [nvarchar](50) NULL,
	[CarServiceDate] [date] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[ServiceStatusId] [int] NOT NULL,
	[IsPaid] [bit] NOT NULL,
 CONSTRAINT [PK_Garage_CarDaySetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Default [DF__Garage_Ca__Servi__58D1301D]    Script Date: 10/13/2016 00:01:18 ******/
ALTER TABLE [dbo].[Garage_CarDaySetting] ADD  CONSTRAINT [DF__Garage_Ca__Servi__58D1301D]  DEFAULT ((0)) FOR [ServiceStatusId]