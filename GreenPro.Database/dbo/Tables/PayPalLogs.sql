CREATE TABLE [dbo].[PayPalLogs]
(
	[LogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(50) NULL, 
    [ApiSatus] NVARCHAR(50) NULL, 
    [ResponseError] NVARCHAR(256) NULL, 
    [ResponseRedirectURL] NVARCHAR(100) NULL, 
    [ECToken] NVARCHAR(100) NULL, 
    [BillingAggrementID] NVARCHAR(100) NULL, 
    [TimeStamp] VARCHAR(50) NULL, 
    [CorrelationID] VARCHAR(50) NULL, 
    [ACK] VARCHAR(50) NULL, 
    [ServerDate] DATETIME NULL, 
    [SubscriptionID] INT NULL, 
    CONSTRAINT [FK_PayPalLogs_UserPackage] FOREIGN KEY (SubscriptionID) REFERENCES UserPackages(ID)
)
