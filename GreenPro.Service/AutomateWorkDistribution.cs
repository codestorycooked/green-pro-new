using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Service
{
    class AutomateWorkDistribution
    {
        public void DistributeCars()
        {
            using (var db = new GreenProDbEntities())
            {
                var currentDay = DateTime.Now.Date.DayOfWeek.ToString();
                var garageList = db.Garages.Where(a => a.ServiceDays == currentDay).ToList();
                var modelList = new List<CrewAdminGarageCarModel>();

                var list = db.LeaderGarageDays.Where(a => a.Day == currentDay).ToList();
                db.LeaderCarJobs.RemoveRange(db.LeaderCarJobs.ToList());
                db.SaveChanges();
                foreach (var garage in garageList)
                {
                    var carList = (
                                        from p in db.Packages
                                        from u in db.UserPackages
                                        from c in db.CarUsers
                                        where u.PackageId == p.PackageId && u.CarId == c.CarId && c.GarageId == garage.GarageId
                                        select c);

                    var leaderList = (from a in db.AspNetUsers
                                      from b in db.WorkerGarages
                                      from c in db.LeaderGarageDays
                                      where c.Day == currentDay && c.WorkerGarageID == b.Id && b.IsLeader == true && b.GarageID == garage.GarageId && a.Id == b.CrewLeaderId
                                      select a).ToList();

                    int leaderCount = leaderList.Count();
                    if (leaderCount <= 0)
                        continue;
                    int counter = 1;
                    int count = 0;
                    var maxCount = db.GarageMaxCars.Where(a => a.GarageID == garage.GarageId && a.DayRef == currentDay).Select(a => a.CarPerCrewAdmin).FirstOrDefault();
                    foreach (var car in carList)
                    {
                        if (counter > leaderCount)
                        {
                            counter = 1;
                            count++;
                            if (count == maxCount)
                            {
                                var packageDetails = db.UserPackages.FirstOrDefault(a => a.CarId == car.CarId);
                                db.UnAssignedCars.Add(new UnAssignedCar { GarageID = garage.GarageId, DateTime = DateTime.Now, PackageId = packageDetails.PackageId });
                                continue;
                            }
                        }

                        db.LeaderCarJobs.Add(new LeaderCarJob { Day = currentDay, GarageId = garage.GarageId, LeaderId = leaderList[counter - 1].Id, CarId = car.CarId });
                        counter++;

                    }
                }
                db.SaveChanges();
            }
        }
    }
}
