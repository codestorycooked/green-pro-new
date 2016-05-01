CREATE TABLE [dbo].[LeaderMember]
(
ID int primary key identity,
    [CrewLeaderID] NVARCHAR(128) NOT NULL, 
    [CrewMemberID] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_CrewLeader_AspNetUSer] FOREIGN KEY (CrewleaderID) REFERENCES ASpnetUSers(Id),
	CONSTRAINT [FK_CrewMember_AspNetUSer] FOREIGN KEY (CrewMemberID) REFERENCES ASpnetUSers(Id)
)
