CREATE TABLE [dbo].[CarServiceEntries](
	[Id] [int] NOT NULL,
	[CarOwnerId] [nvarchar](128) NOT NULL,
	[CarId] [int] NOT NULL,
	[WorkerId] [nvarchar](128) NOT NULL,
	[PackageId] [int] NOT NULL,
	[Comments] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_CarOwnerId_AspNetUsers]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CarServiceEntries]  WITH CHECK ADD  CONSTRAINT [FK_CarOwnerId_AspNetUsers] FOREIGN KEY([CarOwnerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[CarServiceEntries] CHECK CONSTRAINT [FK_CarOwnerId_AspNetUsers]
GO
/****** Object:  ForeignKey [FK_CarServiceEntries_CarUsers]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CarServiceEntries]  WITH CHECK ADD  CONSTRAINT [FK_CarServiceEntries_CarUsers] FOREIGN KEY([CarId])
REFERENCES [dbo].[CarUsers] ([CarId])
GO

ALTER TABLE [dbo].[CarServiceEntries] CHECK CONSTRAINT [FK_CarServiceEntries_CarUsers]
GO
/****** Object:  ForeignKey [FK_CarServiceEntries_Packages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CarServiceEntries]  WITH CHECK ADD  CONSTRAINT [FK_CarServiceEntries_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO

ALTER TABLE [dbo].[CarServiceEntries] CHECK CONSTRAINT [FK_CarServiceEntries_Packages]
GO
/****** Object:  ForeignKey [FK_WorkererId_AspNetUsers]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[CarServiceEntries]  WITH CHECK ADD  CONSTRAINT [FK_WorkererId_AspNetUsers] FOREIGN KEY([WorkerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[CarServiceEntries] CHECK CONSTRAINT [FK_WorkererId_AspNetUsers]