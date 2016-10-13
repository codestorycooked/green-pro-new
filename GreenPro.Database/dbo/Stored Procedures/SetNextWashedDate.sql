create proc [dbo].[SetNextWashedDate]
as
update UserPackages set lastServiceDate = nextServiceDate where nextServicedate <=GETDATE()

update UserPackages set nextServiceDate = case [SubscriptionTypeId]
											when 1 then dateadd(dd,7,lastServiceDate)
											when 2 then dateadd(dd,14,lastServiceDate)
											when 3 then dateadd(dd,28,lastServiceDate)
											end