CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [DateofBirth] DATE NOT NULL, 
    [Address] NVARCHAR(512) NOT NULL, 
    [State] INT NOT NULL, 
    [City] INT NOT NULL, 
    [Pincode] NVARCHAR(20) NOT NULL, 
    [Balance] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_AspNetUsers_States] FOREIGN KEY ([State]) REFERENCES [States]([Id]),
	CONSTRAINT [FK_AspNetUsers_Cities] FOREIGN KEY ([City]) REFERENCES [Cities]([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);

