CREATE TABLE [dbo].[AutomaticPayments]
(
	[Id] INT NOT NULL PRIMARY KEY IDentity, 
    [UserID] NVARCHAR(155) NULL, 
    [UserPackageID] INT NULL, 
    [TotalAmount] MONEY NULL, 
    [Remarks] NVARCHAR(255) NULL, 
    [TransactionDate] DATETIME NULL, 
    [PaypalBillingID] NVARCHAR(50) NULL, 
    [PayPalECToken] NVARCHAR(50) NULL, 
    [AdhocUserPackageID] INT NULL
)
