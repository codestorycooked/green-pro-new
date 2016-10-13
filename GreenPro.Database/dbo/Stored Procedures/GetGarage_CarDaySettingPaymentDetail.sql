CREATE Proc [dbo].[GetGarage_CarDaySettingPaymentDetail]
@ServiceDate Date
as

select u.id as UserPackageID, 
--u.CarId, 
u.[IsActive], s.Id as JobId, CONVERT(INT, s.[EntityTypeValue]) as CarId, s.CarServiceDate,
CASE (select count(*) from PaypalAutoPayments pa where pa.[UserPackageID]=u.id and pa.ispaid=1 and CONVERT(VARCHAR(10),pa.ServiceDate,101) = CONVERT(VARCHAR(10),@ServiceDate,101))  
WHEN 1 THEN cast(1 as bit) 
ELSE cast(0 as bit) END as IsPaid,
--isnull(p.IsPaid,0) as IsPaid,
pl.LogId ,pl.BillingAggrementID ,pl.ECToken ,pl.CorrelationID from [dbo].[UserPackages] u inner join 
dbo.PayPalLogs pl
on u.id=pl.SubscriptionID inner join
 [dbo].[Garage_CarDaySetting] s
 on u.[CarId]=CONVERT(INT, s.[EntityTypeValue])
   left join [dbo].[PaypalAutoPayments] p
   on u.id=p.[UserPackageID]
where s.entityTypeKey=3
and u.[IsActive]=1
and isnull(pl.BillingAggrementID,'')<>''
and CONVERT(VARCHAR(10),s.[CarServiceDate],101) = CONVERT(VARCHAR(10),@ServiceDate,101)
 --order by id desc