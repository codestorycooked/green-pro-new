CREATE TABLE [dbo].[Cities]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [CityName] NVARCHAR(100) NULL, 
    [StateID] INT NULL, 
    CONSTRAINT [FK_Cities_State] FOREIGN KEY (StateID) REFERENCES States(ID)
)


Go