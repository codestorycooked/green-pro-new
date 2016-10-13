CREATE TABLE [dbo].[CrewAdminMember](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CrewMemberId] [nvarchar](128) NOT NULL,
	[CrewAdminId] [nvarchar](128) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_CrewAdmin_AspNetUsers]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CrewAdminMember]  WITH CHECK ADD  CONSTRAINT [FK_CrewAdmin_AspNetUsers] FOREIGN KEY([CrewMemberId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[CrewAdminMember] CHECK CONSTRAINT [FK_CrewAdmin_AspNetUsers]
GO
/****** Object:  ForeignKey [FK_CrewMember_AspNetUsers]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CrewAdminMember]  WITH CHECK ADD  CONSTRAINT [FK_CrewMember_AspNetUsers] FOREIGN KEY([CrewAdminId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[CrewAdminMember] CHECK CONSTRAINT [FK_CrewMember_AspNetUsers]