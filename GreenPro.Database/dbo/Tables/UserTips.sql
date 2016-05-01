CREATE TABLE [dbo].[UserTips]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [TIPAMOUNT] MONEY NOT NULL, 
    [USERPACKAGEID] INT NOT NULL, 
    CONSTRAINT [FK_UserTips_UserPackages] FOREIGN KEY (userpackageid) REFERENCES Userpackages(id)
)
