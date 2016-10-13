CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](100) NULL,
	[StateID] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Cities_State]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[States] ([Id])
GO

ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_State]