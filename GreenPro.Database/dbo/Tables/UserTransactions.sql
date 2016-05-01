CREATE TABLE [dbo].[UserTransactions]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [TransactionDate] DATETIME NOT NULL, 
    [Amount] NUMERIC(18, 2) NOT NULL, 
    [PackageId] INT NULL, 
    [PaypalId] NVARCHAR(50) NOT NULL, 
    [Details] NVARCHAR(MAX) NOT NULL, 
    [Userid] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_UserTransactions_UserPackages] FOREIGN KEY ([PackageId]) REFERENCES [USerPackages](id), 
    CONSTRAINT [FK_UserTransactions_ASpnetUsers] FOREIGN KEY (Userid) REFERENCES [ASpnetusers]([id])
)
