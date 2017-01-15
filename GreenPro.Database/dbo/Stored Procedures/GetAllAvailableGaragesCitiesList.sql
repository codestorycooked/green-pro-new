CREATE proc [dbo].[GetAllAvailableGaragesCitiesList]
as
select c.Id, c.CityName as CityName, s.Id as StateId from Cities c inner join [dbo].[States] s
on c.StateID=s
.Id
where c.id in (select distinct city from [dbo].[Garages])
AND c.IsActive=1
Order by c.CityName,s.StateName