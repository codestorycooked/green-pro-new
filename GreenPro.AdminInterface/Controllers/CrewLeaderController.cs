using System;
using System.Linq;
using System.Web.Mvc;
using GreenPro.AdminInterface.ViewModels;
using GreenPro.Data;
using Microsoft.AspNet.Identity;
using GreenPro.Data.Models;
using GreenPro.AdminInterface.Helper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace GreenPro.AdminInterface.Controllers
{
    public class CrewLeaderController : BaseController
    {
        private GreenProDbEntities db;
        WorkflowMessageService _workflowMessageService;
        public CrewLeaderController()
        {
            db = new GreenProDbEntities();
            _workflowMessageService = new WorkflowMessageService();

        }

        void PrepareLeaderSerivceDayInformation(LeaderServiceDayViewModel model, string serviceDay, DateTime serviceDate, string userid)
        {
            int TeamId = 0;
            model.ServiceDay = serviceDay;
            var result = db.Garage_CarDaySetting.Where(q => q.CarServiceDate == serviceDate
                && q.ServiceDay == serviceDay && q.EntityTypeKey == (int)EntityTypeKey.Leader
                && q.EntityTypeValue == userid).FirstOrDefault();
            if (result != null)
            {
                model.ServiceDayRecordExists = true;

                TeamId = result.GarageTeamId;
                // Get all information like - leader, Member, Cars for the current day/

                var TeamResultForDay = db.Garage_CarDaySetting.Where(q => q.CarServiceDate == serviceDate
                    && q.ServiceDay == serviceDay
                    && q.GarageTeamId == TeamId).ToList();


                // Get Team Information.
                var team = db.GarageTeams.Where(t => t.Id == TeamId).SingleOrDefault();
                model.TeamId = team.Id;
                model.TeamName = team.Title;


                // Get Garage Information.
                var garage = db.Garages.Where(g => g.GarageId == team.GarageId).SingleOrDefault();
                model.GarageId = garage.GarageId;
                model.GarageName = garage.Garage_Name;


                // Get Leader Information.
                var leader = db.AspNetUsers.Where(u => u.Id == userid).SingleOrDefault();
                model.LeaderId = leader.Id;
                model.LeaderName = leader.FirstName + " " + leader.LastName;


                // Get Leader Member Information.
                var memberList = db.Garage_CarDaySetting.Where(q => q.CarServiceDate == serviceDate
                    && q.ServiceDay == serviceDay
                    && q.EntityTypeKey == (int)EntityTypeKey.Member
                    && q.GarageTeamId == model.TeamId).ToList();

                if (memberList.Count > 0)
                {
                    model.Members = new List<TeamMember>();
                    foreach (var member in memberList)
                    {
                        var aspMember = db.AspNetUsers.Where(u => u.Id == member.EntityTypeValue).SingleOrDefault();
                        if (aspMember != null)
                            model.Members.Add(new TeamMember()
                            {
                                MemberId = aspMember.Id,
                                MemberName = aspMember.FirstName + " " + aspMember.LastName

                            });
                    }
                }


                // Get Car Information
                var CarList = db.Garage_CarDaySetting.Where(q => q.CarServiceDate == serviceDate
                    && q.ServiceDay == serviceDay
                    && q.EntityTypeKey == (int)EntityTypeKey.Car
                    && q.IsPaid==true
                    && q.GarageTeamId == model.TeamId).ToList();

                if (CarList.Count > 0)
                {
                    model.CarServicesList = new List<CarServices>();
                    foreach (var car in CarList)
                    {
                        int carId = Convert.ToInt32(car.EntityTypeValue);
                        var aspCar = db.CarUsers.Where(c => c.CarId == carId).SingleOrDefault();
                        if (aspCar != null)
                        {
                            CarServices carService = new CarServices()
                            {
                                ServiceDayId = car.Id,
                                CarId = aspCar.CarId,
                                CarDisplayName = aspCar.DisplayName,
                                Color = aspCar.Color,
                                Make=aspCar.Make,
                                PurchaseYear=aspCar.PurchaseYear,
                                LicenseNumber = aspCar.LicenseNumber,
                                serviceStatus = (ServiceStatus)car.ServiceStatusId


                            };


                            //// Get Car Services Information
                            //carService.SelectServices = new List<CarServices.SelectService>();
                            //var pCarId = new SqlParameter();
                            //pCarId.ParameterName = "CarId";
                            //pCarId.Value = aspCar.CarId;
                            //pCarId.DbType = DbType.Int32;

                            //var servicesList = db.Database.SqlQuery<GreenPro.Data.Service>(
                            //                       "EXEC dbo.GetServicesByCarId @CarId",
                            //                       pCarId
                            //                       ).ToList();

                            //if (servicesList.Count > 0)
                            //{
                            //    foreach (var service in servicesList)
                            //    {
                            //        CarServices.SelectService seviceModel = new CarServices.SelectService();
                            //        seviceModel.ServiceName = service.Service_Name;
                            //        carService.SelectServices.Add(seviceModel);
                            //    }
                            //}

                            model.CarServicesList.Add(carService);
                        }
                    }
                }


            }
            else
            {
                model.ServiceDayRecordExists = false;
            }


        }

        // GET: CrewLeader
        public ActionResult Index()
        {
            LeaderServiceDayViewModel model = new LeaderServiceDayViewModel();
            var userid = User.Identity.GetUserId();
            DateTime serviceDate = DateTime.Now.Date;
            var serviceDay = DateTime.Now.Date.DayOfWeek.ToString();

            PrepareLeaderSerivceDayInformation(model, serviceDay, serviceDate, userid);

            return View(model);
        }

        [HttpPost, ActionName("Index")]
        [FormValueRequired("btnSaveLeaders")]
        public ActionResult Index(FormCollection fc)
        {
            LeaderServiceDayViewModel model = new LeaderServiceDayViewModel();

            return View();
        }


        [HttpPost, ActionName("Index")]
        [FormValueRequired("btnTomorroww")]
        public ActionResult IndexTomorrow()
        {
            LeaderServiceDayViewModel model = new LeaderServiceDayViewModel();

            DateTime serviceDate = DateTime.Now.AddDays(1).Date;
            var serviceDay = DateTime.Now.AddDays(1).Date.DayOfWeek.ToString();
            var userid = User.Identity.GetUserId();
            PrepareLeaderSerivceDayInformation(model, serviceDay, serviceDate, userid);

            return View(model);
        }


        public ActionResult CurrentCarServiceDetail(int Id)
        {
            CurrentCarServiceDetailModel model = new CurrentCarServiceDetailModel();

            // Get Car Information
            var CarServiceDetail = db.Garage_CarDaySetting.Where(q => q.Id == Id
                && q.EntityTypeKey == (int)EntityTypeKey.Car
                ).SingleOrDefault();
            if (CarServiceDetail == null)
            {
                return RedirectToAction("Index");
            }
            model.CurrentCarServiceId = Id;
            int carId = Convert.ToInt32(CarServiceDetail.EntityTypeValue);
            var aspCar = db.CarUsers.Where(c => c.CarId == carId).SingleOrDefault();
            if (aspCar != null)
            {
                model.CarService = new CarServices()
                {
                    ServiceDayId = CarServiceDetail.Id,
                    CarId = aspCar.CarId,
                    CarDisplayName = aspCar.DisplayName,
                    Make=aspCar.Make,
                    PurchaseYear=aspCar.PurchaseYear,
                    Color = aspCar.Color,
                    LicenseNumber = aspCar.LicenseNumber,
                    serviceStatus = (ServiceStatus)CarServiceDetail.ServiceStatusId,
                    //ServiceStatusId = CarServiceDetail.ServiceStatusId,

                };
                model.ServiceStatusId = CarServiceDetail.ServiceStatusId;

                if (CarServiceDetail.StartTime.HasValue)
                    model.StartDateTime = CarServiceDetail.StartTime;

                if (CarServiceDetail.EndTime.HasValue)
                    model.EndDateTime = CarServiceDetail.EndTime;

                model.Comment = CarServiceDetail.Comment;

                // Get Car Services Information
                model.CarService.SelectServices = new List<CarServices.SelectService>();
                var pCarId = new SqlParameter();
                pCarId.ParameterName = "CarId";
                pCarId.Value = aspCar.CarId;
                pCarId.DbType = DbType.Int32;

                var servicesList = db.Database.SqlQuery<GreenPro.Data.Service>(
                                       "EXEC dbo.GetServicesByCarId @CarId",
                                       pCarId
                                       ).ToList();

                if (servicesList.Count > 0)
                {
                    foreach (var service in servicesList)
                    {
                        CarServices.SelectService seviceModel = new CarServices.SelectService();
                        seviceModel.ServiceName = service.Service_Name;
                        model.CarService.SelectServices.Add(seviceModel);
                    }
                }
            }

            model.AvailableServiceStatus = ListHelper.GetServiceStatusList();



            return View(model);
        }


        [HttpPost, ActionName("CurrentCarServiceDetail")]
        [FormValueRequired("btnStartJob")]
        public ActionResult CurrentCarServiceStartJob(CurrentCarServiceDetailModel model)
        {
            // Get Car Information
            var CarServiceDetail = db.Garage_CarDaySetting.Where(q => q.Id == model.CurrentCarServiceId
                && q.EntityTypeKey == (int)EntityTypeKey.Car
                ).SingleOrDefault();
            if (CarServiceDetail == null)
            {
                return RedirectToAction("Index");
            }
            CarServiceDetail.ServiceStatusId = (int)ServiceStatus.Processing;
            CarServiceDetail.StartTime = DateTime.Now;                        
            db.SaveChanges();

            return RedirectToAction("CurrentCarServiceDetail", "CrewLeader", new { id = model.CurrentCarServiceId });


            
        }

        [HttpPost, ActionName("CurrentCarServiceDetail")]
        [FormValueRequired("btnEndJob")]
        public ActionResult CurrentCarServiceEndJob(CurrentCarServiceDetailModel model)
        {
            

            // Get Car Information
            var CarServiceDetail = db.Garage_CarDaySetting.Where(q => q.Id == model.CurrentCarServiceId
                && q.EntityTypeKey == (int)EntityTypeKey.Car
                ).SingleOrDefault();
            if (CarServiceDetail == null)
            {
                return RedirectToAction("Index");
            }
            CarServiceDetail.ServiceStatusId = (int)ServiceStatus.Complated;
            CarServiceDetail.EndTime = DateTime.Now;
            db.SaveChanges();
            string ServiceName = string.Empty;
            int carId = Convert.ToInt32(CarServiceDetail.EntityTypeValue);

          


            var aspCar = db.CarUsers.Where(c => c.CarId == carId).SingleOrDefault();
            if (aspCar != null)
            {
                // Set Last and Next service date userPackage
                var activeUserPackage = aspCar.UserPackages.Where(u => u.PaymentRecieved == true && u.IsActive == true).FirstOrDefault();
                if (activeUserPackage != null)
                {
                    activeUserPackage.LastServiceDate = activeUserPackage.NextServiceDate;
                    activeUserPackage.NextServiceDate = Convert.ToDateTime(activeUserPackage.NextServiceDate).AddDays(activeUserPackage.SubscriptionTypeId * 7);
                }

                model.CarService = new CarServices()
                {
                    ServiceDayId = CarServiceDetail.Id,
                    CarId = aspCar.CarId,
                    CarDisplayName = aspCar.DisplayName,
                    Color = aspCar.Color,
                    LicenseNumber = aspCar.LicenseNumber,
                    serviceStatus = (ServiceStatus)CarServiceDetail.ServiceStatusId,                   

                };


                // Get Car Services Information
                model.CarService.SelectServices = new List<CarServices.SelectService>();
                var pCarId = new SqlParameter();
                pCarId.ParameterName = "CarId";
                pCarId.Value = aspCar.CarId;
                pCarId.DbType = DbType.Int32;

                var servicesList = db.Database.SqlQuery<GreenPro.Data.Service>(
                                       "EXEC dbo.GetServicesByCarId @CarId",
                                       pCarId
                                       ).ToList();


               
                if (servicesList.Count > 0)
                {
                    ServiceName += "<ul>";
                    foreach (var service in servicesList)
                    {
                        CarServices.SelectService seviceModel = new CarServices.SelectService();
                        seviceModel.ServiceName = service.Service_Name;
                        model.CarService.SelectServices.Add(seviceModel);
                        ServiceName += "<li>" + service.Service_Name + "</li>";
                    }

                    ServiceName += "</ul>";
                }
            }

            var userInfo = aspCar.AspNetUser;

            _workflowMessageService.SendCarServiceCompletionNotificationtoCustomer(userInfo.UserName, userInfo.Email, ServiceName);
            return RedirectToAction("CurrentCarServiceDetail", "CrewLeader", new { id = model.CurrentCarServiceId });



        }




        [HttpPost]
        [FormValueRequired("Save")]
        public ActionResult CurrentCarServiceDetail(CurrentCarServiceDetailModel model)
        {
            // Get Car Information
            var CarServiceDetail = db.Garage_CarDaySetting.Where(q => q.Id == model.CurrentCarServiceId
                && q.EntityTypeKey == (int)EntityTypeKey.Car
                ).SingleOrDefault();
            if (CarServiceDetail == null)
            {
                return RedirectToAction("Index");
            }            

            CarServiceDetail.Comment = model.Comment;
            db.SaveChanges();

            return RedirectToAction("CurrentCarServiceDetail", "CrewLeader", new { id = model.CurrentCarServiceId });
            
        }

        public ActionResult PackageDetails(string id)
        {
            int carId = Convert.ToInt32(id);
            var details = db.UserPackages.FirstOrDefault(a => a.CarId == carId && a.PaymentRecieved);

            return View(details);
        }

        public ActionResult MyCrew()
        {
            var userid = User.Identity.GetUserId();
            //Get all garages for logged in user:
            //var garages = db.WorkerGarages.Where(a => a.CrewLeaderId == userid).ToList();
            var garages = db.WorkerGarages.FirstOrDefault(a => a.CrewLeaderId == userid);
            CrewMemberAssignment crewMembers = new CrewMemberAssignment
            {
                PickedMembers = db.LeaderMembers.Where(a => a.CrewLeaderID == userid).ToList()
            };





            return View(crewMembers);
        }

        public ActionResult AvailableCrew()
        {
            var userid = User.Identity.GetUserId();
            //Get all garages for logged in user:
            //var garages = db.WorkerGarages.Where(a => a.CrewLeaderId == userid).ToList();
            var garages = db.WorkerGarages.FirstOrDefault(a => a.CrewLeaderId == userid);
            CrewMemberAssignment crewMembers = new CrewMemberAssignment
            {
                AvailableMembers = db.WorkerGarages.Where(a => a.IsLeader == false && a.GarageID == garages.GarageID).ToList()
            };
            return View(crewMembers);
        }
        public ActionResult AddMember(string id)
        {
            var userid = User.Identity.GetUserId();
            var isPresent = db.LeaderMembers.FirstOrDefault(a => a.CrewMemberID == id && a.CrewLeaderID == userid);
            if (id != null && isPresent == null)
            {
                LeaderMember member = new LeaderMember { CrewLeaderID = userid, CrewMemberID = id };
                db.LeaderMembers.Add(member);
                db.SaveChanges();
                return RedirectToAction("MyCrew");
            }
            return RedirectToAction("AvailableCrew");
        }

        public ActionResult RemoveMember(string id)
        {
            var userid = User.Identity.GetUserId();
            var isPresent = db.LeaderMembers.FirstOrDefault(a => a.CrewMemberID == id && a.CrewLeaderID == userid);
            if (id != null && isPresent != null)
            {
                //  LeaderMember member = new LeaderMember { CrewLeaderID = userid, CrewMemberID = id };
                db.LeaderMembers.Remove(isPresent);
                db.SaveChanges();
                return RedirectToAction("MyCrew");
            }
            return RedirectToAction("AvailableCrew");
        }

        public ActionResult StartWork(string packageID)
        {
            if (packageID == null) throw new ArgumentNullException("packageID");
            int packageId = Convert.ToInt32(packageID);
            var getPackage = db.UserPackages.FirstOrDefault(a => a.PackageId == packageId);
            var userid = User.Identity.GetUserId();
            if (getPackage != null)
            {
                //GEt members for Currently logged in Members
                ViewBag.getMembers = db.CrewAdminMembers.Where(a => a.CrewAdminId == userid);
            }
            return View();
        }
    }
}