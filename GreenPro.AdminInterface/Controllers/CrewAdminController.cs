using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GreenPro.AdminInterface.ViewModels;
using GreenPro.Data;

namespace GreenPro.AdminInterface.Controllers
{
    public class CrewAdminController : BaseController
    {
        private GreenProDbEntities db;
        public CrewAdminController()
        {
            db = new GreenProDbEntities();
        }

        public ActionResult Index()
        {
            //var currentDay = DateTime.Now.Date.DayOfWeek.ToString();
            var currentDay = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString();
            var garageList = db.Garages.Where(a => a.ServiceDays.Contains(currentDay)).ToList();
            var modelList = new List<CrewAdminGarageCarModel>();
            ViewData["WorkingDay"] = DateTime.Now.Date.AddDays(1).ToLongDateString();

            foreach (var garage in garageList)
            {
                

                var carCount = (
                from c in db.CarUsers
                join
                    up in db.UserPackages
                    on c.CarId equals up.CarId
                join p in db.Packages
                on up.PackageId equals p.PackageId
                where c.GarageId == garage.GarageId && up.PaymentRecieved == true && up.ServiceDay == currentDay
                select new { CarId = c.CarId, DisplayName = c.DisplayName }).ToList().Count;

                var model = new CrewAdminGarageCarModel { GarageId = garage.GarageId, Garage_Address = garage.Garage_Address, Garage_Name = garage.Garage_Name, CarCount = carCount };
                modelList.Add(model);
            }
            return View(modelList);
        }

        public ActionResult AssignLeaders(int? id)
        {
            var modelList = new List<CrewAdminAssignLeaderModel>();
            var leaders = db.WorkerGarages.Where(a => a.GarageID == id && a.IsLeader).ToList();
            foreach (var leader in leaders)
            {
                var leaderDetails = db.AspNetUsers.FirstOrDefault(a => a.Id == leader.CrewLeaderId);
                var leaderWorkerDay = db.LeaderGarageDays.FirstOrDefault(a => a.WorkerGarageID == leader.Id);
                bool isAssigned = leaderWorkerDay != null;
                if (leaderDetails == null)
                {
                    break;
                }
                if (id == null) continue;
                var model = new CrewAdminAssignLeaderModel { GarageId = id.Value, Id = leaderDetails.Id, FirstName = leaderDetails.FirstName, LastName = leaderDetails.LastName, IsLeader = isAssigned, WorkerId = leader.Id };
                modelList.Add(model);
            }
            return View(modelList);
        }

        [HttpPost]
        public ActionResult AssignLeaders(List<CrewAdminAssignLeaderModel> model)
        {
            var day = DateTime.Now.DayOfWeek.ToString();
            var leaderGarageDay = new List<LeaderGarageDay>();

            foreach (var item in model)
            {
                //var workerGarage = db.WorkerGarages.FirstOrDefault(a => a.GarageID == item.GarageId && a.CrewLeaderId == item.Id);
                //var leaderGarageDaymodel = new LeaderGarageDay { Day = day, WorkerGarageID = item.GarageId };
                var isPresent = db.LeaderGarageDays.FirstOrDefault(a => a.WorkerGarageID == item.WorkerId);
                if (item.IsLeader == true)
                {
                    if (isPresent != null) continue;
                    var addmodel = new LeaderGarageDay { WorkerGarageID = item.WorkerId, Day = day };
                    db.LeaderGarageDays.Add(addmodel);
                    db.SaveChanges();
                }
                else
                {
                    //remove
                    if (isPresent == null) continue;
                    db.LeaderGarageDays.Remove(isPresent);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult AssignCarPerLeader(int? id)
        {
            var day = DateTime.Now.Date.DayOfWeek.ToString();
            var model = db.GarageMaxCars.FirstOrDefault(a => a.DayRef == day && a.GarageID == id);
            ViewBag.GarageID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult AssignCarPerLeader(GarageMaxCar model, string garageid)
        {

            int garageId = Convert.ToInt32(garageid);
            var day = DateTime.Now.Date.DayOfWeek.ToString();
            model.DayRef = day;
            model.GarageID = Convert.ToInt32(garageid);
            if (ModelState.IsValid)
            {
                var isPresent = db.GarageMaxCars.FirstOrDefault(a => a.DayRef == day && a.GarageID == garageId);
                if (isPresent == null)
                {
                    db.GarageMaxCars.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    isPresent.CarPerCrewAdmin = model.CarPerCrewAdmin;
                    db.Entry(isPresent).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            return RedirectToAction("Index");
        }

        public ActionResult AssignCars()
        {
            return View();
        }


    }
}