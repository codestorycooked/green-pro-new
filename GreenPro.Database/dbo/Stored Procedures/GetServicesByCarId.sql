CREATE Proc [dbo].[GetServicesByCarId]
@CarId int=0 
as

SELECT s.* from UserPackages up inner join Packages p 
on up.PackageId=p.PackageId inner join Package_Services ps
on p.PackageId=ps.PackageID inner join [Services] s
on ps.ServiceID=s.ServiceID
WHERE up.CarId=@CarId and up.PaymentRecieved=1

union

SELECT s.* from UserPackages up inner join UserPackagesAddons upa 
on up.Id=upa.UserPackageID  inner join [Services] s
on upa.ServiceID=s.ServiceID
WHERE up.CarId=@CarId and up.PaymentRecieved=1
and up.NextServiceDate=upa.NextServiceDate