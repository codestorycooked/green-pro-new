CREATE TABLE [dbo].[GargesTimeingSlots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SlotTimeing] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[GarageId] [int] NOT NULL,
 CONSTRAINT [PK_GargesTimeingSlots] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_GargesTimeingSlots_Garages]    Script Date: 10/13/2016 00:01:21 ******/
ALTER TABLE [dbo].[GargesTimeingSlots]  WITH CHECK ADD  CONSTRAINT [FK_GargesTimeingSlots_Garages] FOREIGN KEY([GarageId])
REFERENCES [dbo].[Garages] ([GarageId])
GO

ALTER TABLE [dbo].[GargesTimeingSlots] CHECK CONSTRAINT [FK_GargesTimeingSlots_Garages]