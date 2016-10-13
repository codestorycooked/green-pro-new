CREATE TABLE [dbo].[LeaderMember](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CrewLeaderID] [nvarchar](128) NOT NULL,
	[CrewMemberID] [nvarchar](128) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_CrewLeader_AspNetUSer]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[LeaderMember]  WITH CHECK ADD  CONSTRAINT [FK_CrewLeader_AspNetUSer] FOREIGN KEY([CrewLeaderID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[LeaderMember] CHECK CONSTRAINT [FK_CrewLeader_AspNetUSer]
GO
/****** Object:  ForeignKey [FK_CrewMember_AspNetUSer]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[LeaderMember]  WITH CHECK ADD  CONSTRAINT [FK_CrewMember_AspNetUSer] FOREIGN KEY([CrewMemberID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[LeaderMember] CHECK CONSTRAINT [FK_CrewMember_AspNetUSer]