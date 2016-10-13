CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[DateofBirth] [date] NULL,
	[Address] [nvarchar](512) NULL,
	[State] [int] NULL,
	[City] [int] NULL,
	[Pincode] [nvarchar](20) NULL,
	[Balance] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 [ProfilePic] NTEXT NULL, 
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_AspNetUsers_Cities]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Cities] FOREIGN KEY([City])
REFERENCES [dbo].[Cities] ([Id])
GO

ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Cities]
GO
/****** Object:  ForeignKey [FK_AspNetUsers_States]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_States] FOREIGN KEY([State])
REFERENCES [dbo].[States] ([Id])
GO

ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_States]
GO
/****** Object:  Default [DF__AspNetUse__Balan__04E4BC85]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__AspNetUse__Balan__04E4BC85]  DEFAULT ((0)) FOR [Balance]
GO
/****** Object:  Default [DF_AspNetUsers_IsDelete]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]