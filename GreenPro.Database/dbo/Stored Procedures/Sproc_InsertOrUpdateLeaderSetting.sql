CREATE proc [dbo].[Sproc_InsertOrUpdateLeaderSetting]
@GarageTeamId int=0,
@EntityTypeKey int,
	@EntityTypeValues		nvarchar(MAX) = null,	--a list of (comma-separated list). e.g. 1,2,3	
	@GarageId			int = 0,
	@ServiceDay			nvarchar(50) = null
	as

	SET @EntityTypeValues = isnull(@EntityTypeValues, '')	
	CREATE TABLE #FilteredLeaderIds
	(
		EntityValue nvarchar(128) not null
	)
	INSERT INTO #FilteredLeaderIds (EntityValue)
	SELECT data FROM [Splitstring_to_table](@EntityTypeValues, ',')

	Delete [dbo].[Garage_LeaderSetting] WHERE [GarageId]=@GarageId and [ServiceDay]=@ServiceDay and [GarageTeamId]=@GarageTeamId and [EntityTypeKey]=@EntityTypeKey

	INSERT INTO [dbo].[Garage_LeaderSetting]
           ([GarageTeamId]
		   ,[EntityTypeKey]
		   ,[EntityTypeValue]
           ,[GarageId]
           ,[ServiceDay]
           ,[CreatedDate])
     SELECT @GarageTeamId,@EntityTypeKey,EntityValue,@GarageId,@ServiceDay,GETDATE() FROM  #FilteredLeaderIds
	
	Drop Table #FilteredLeaderIds