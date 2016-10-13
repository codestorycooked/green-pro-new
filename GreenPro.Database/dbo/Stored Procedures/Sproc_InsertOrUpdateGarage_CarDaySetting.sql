CREATE proc [dbo].[Sproc_InsertOrUpdateGarage_CarDaySetting]
@GarageTeamId int=0,
@EntityTypeKey int,
	@EntityTypeValues		nvarchar(MAX) = null,	--a list of (comma-separated list). e.g. 1,2,3	
	@GarageId			int = 0,
	@ServiceDay			nvarchar(50) = null,
	@CarServiceDate Date,
	@IsLocked bit=0,
	@IsPaid bit=0
	as

	SET @EntityTypeValues = isnull(@EntityTypeValues, '')	
	CREATE TABLE #FilteredLeaderIds
	(
		EntityValue nvarchar(128) not null
	)
	INSERT INTO #FilteredLeaderIds (EntityValue)
	SELECT data FROM [Splitstring_to_table](@EntityTypeValues, ',')

	Delete [dbo].[Garage_CarDaySetting] WHERE [GarageId]=@GarageId and [ServiceDay]=@ServiceDay 
	and [GarageTeamId]=@GarageTeamId and [EntityTypeKey]=@EntityTypeKey and [IsLocked]=0
	and CONVERT(VARCHAR(10),CarServiceDate,101)=CONVERT(VARCHAR(10),@CarServiceDate,101)

	INSERT INTO [dbo].[Garage_CarDaySetting]
           ([GarageTeamId]
		   ,[EntityTypeKey]
		   ,[EntityTypeValue]
           ,[GarageId]
           ,[ServiceDay]
           ,[CreatedDate]
		   ,[CarServiceDate]
		   ,[IsLocked]
		   ,[IsPaid])
     SELECT @GarageTeamId,@EntityTypeKey,EntityValue,@GarageId,@ServiceDay,GETDATE(),@CarServiceDate,@IsLocked,@IsPaid FROM  #FilteredLeaderIds
	
	Drop Table #FilteredLeaderIds