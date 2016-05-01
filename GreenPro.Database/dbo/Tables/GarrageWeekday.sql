CREATE TABLE [dbo].[GarrageWeekday]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [GarrageId] INT NOT NULL, 
    [WeekdayId] INT NOT NULL, 
    CONSTRAINT [FK_GarrageWeekday_Garages] FOREIGN KEY ([GarrageId]) REFERENCES [Garages]([GarageId]), 
    CONSTRAINT [FK_GarrageWeekday_Weekdays] FOREIGN KEY ([WeekdayId]) REFERENCES [Weekdays]([Id])
)
