CREATE TABLE [dbo].[PaypalAutoPayments]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [TrasactionID] NVARCHAR(150) NULL, 
    [ReferenceID] NVARCHAR(150) NULL, 
    [PaymentStatus] NVARCHAR(50) NULL, 
    [PendingReason] NVARCHAR(150) NULL, 
    [PaymentDate] NVARCHAR(150) NULL, 
    [GrossAmount] NVARCHAR(150) NULL, 
    [UserPackageID] INT NULL, 
    [UserID] nvarchar(128) NULL, 
    [TransactionDate] DATETIME NULL, 
    CONSTRAINT [FK_PaypalAutoPayments_UserPackageID] FOREIGN KEY (UserPackageID) REFERENCES UserPackages(ID), 
    CONSTRAINT [FK_PaypalAutoPayments_AspNEtUSERs] FOREIGN KEY (UserID) REFERENCES ASpnetUSers(ID),

)
