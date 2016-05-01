CREATE TABLE [dbo].[CrewAdminMember]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CrewMemberId] NVARCHAR(128) NOT NULL, 
    [CrewAdminId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_CrewAdmin_AspNetUsers] FOREIGN KEY (CrewMemberId) REFERENCES AspNetUsers(Id),
	CONSTRAINT [FK_CrewMember_AspNetUsers] FOREIGN KEY (CrewAdminId) REFERENCES AspNetUsers(Id)
)
