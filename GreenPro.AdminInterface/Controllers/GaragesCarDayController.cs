using GreenPro.AdminInterface.Helper;
using GreenPro.AdminInterface.ViewModels;
using GreenPro.Data;
using GreenPro.Data.Models;
using GreenPro.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.Controllers
{
    public class GaragesCarDayController : BaseController
    {
        private GreenProDbEntities db;
        WorkflowMessageService _workflowMessageService;
        public GaragesCarDayController()
        {
            db = new GreenProDbEntities();
            _workflowMessageService = new WorkflowMessageService();
        }

        protected void PrepareDefaultSettingModel(DefaultSettingViewModel model, Garage garage, IQueryable<Garage_LeaderSetting> garageDefaultSettingEitity,
            IList<Garage_CarDaySetting> garageCarDaySettingEntity, bool loadDefaultValue = true)
        {


            model.GarageId = garage.GarageId;
            model.GarageName = garage.Garage_Name;
            model.PrepareModelData = true;
            // Teams
            var TeamList = db.GarageTeams.Where(t => t.GarageId == model.GarageId).ToList();

            if (TeamList.Count <= 0)
            {
                model.PrepareModelData = false;
                ErrorNotification("Team not available.");
            }
            foreach (var team in TeamList)
            {
                model.AvailableTeams.Add(new SelectListItem()
                {
                    Text = team.Title,
                    Value = team.Id.ToString()
                });
            }

            //// Crew Leader
            var LeadersList = (from u in db.AspNetUsers
                               join w in db.WorkerGarages
                                                       on u.Id equals w.CrewLeaderId
                               where w.IsLeader == true && u.AspNetRoles.Any(r => r.Name == "Crew Leader")
                               && w.GarageID == model.GarageId
                               select u).ToList();

            if (LeadersList.Count <= 0)
            {
                model.PrepareModelData = false;
                ErrorNotification("Crew Leader not available.");
            }

            foreach (var leader in LeadersList)
            {
                model.AvailableLeaders.Add(new SelectListItem()
                {
                    Text = leader.FirstName + " " + leader.LastName + "<br>" + leader.Email,
                    Value = leader.Id
                });


            }

            // Crew Member

            // var memberList = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Name == "Crew Member")).ToList();
            var memberList = (from u in db.AspNetUsers
                              join w in db.WorkerGarages
                                                      on u.Id equals w.CrewLeaderId
                              where w.IsLeader == false && u.AspNetRoles.Any(r => r.Name == "Crew Member")
                               && w.GarageID == model.GarageId
                              select u).ToList();

            if (memberList.Count <= 0)
            {
                model.PrepareModelData = false;
                ErrorNotification("Crew Member not available.");
            }

            foreach (var member in memberList)
            {
                model.AvailableMembers.Add(new SelectListItem()
                {
                    Text = member.FirstName + " " + member.LastName + "<br>" + member.Email,
                    Value = member.Id
                });


            }

            // Garage Cars


            var garageList = db.Garages.Where(a => a.ServiceDays == model.ServiceDay).ToList();
            var carList = (
                from c in db.CarUsers
                join
                    up in db.UserPackages
                    on c.CarId equals up.CarId
                join p in db.Packages
                on up.PackageId equals p.PackageId
                where c.GarageId == model.GarageId && up.PaymentRecieved == true && up.IsActive == true && up.ServiceDay == model.ServiceDay 
                && up.NextServiceDate==model.ServiceDate
                select new { CarId = c.CarId, DisplayName = c.DisplayName, LicenseNumber = c.LicenseNumber, c.Make, c.Color }).ToList();

            if (carList.Count <= 0)
            {
                model.PrepareModelData = false;
                ErrorNotification("Cars not available.");
            }


            string Description = string.Empty;
            foreach (var car in carList)
            {
                Description = string.Empty;

                Description = car.DisplayName;

                if (!string.IsNullOrEmpty(Description))
                    Description += "<br>" + car.LicenseNumber;

                if (!string.IsNullOrEmpty(Description))
                    Description += "<br>" + car.Make;

                if (!string.IsNullOrEmpty(Description))
                    Description += "<br>" + car.Color;

                model.AvailableCars.Add(new SelectListItem()
                {
                    Text = Description,
                    Value = car.CarId.ToString()
                });
            }


            if (garageCarDaySettingEntity.Count > 0)
            {
                var garageDefaultSettingByDay = garageCarDaySettingEntity.Where(q => q.ServiceDay == model.ServiceDay).ToList();

                if (garageDefaultSettingByDay.Count > 0)
                {
                    // load existing leaders
                    foreach (var leader in LeadersList)
                        foreach (var team in TeamList)
                        {
                            bool selected = garageDefaultSettingByDay.Find(f => f.EntityTypeKey == (int)EntityTypeKey.Leader && f.EntityTypeValue == leader.Id && f.GarageTeamId == team.Id) != null;
                            if (!model.SelectedLeaders.ContainsKey(leader.Id))
                                model.SelectedLeaders[leader.Id] = new Dictionary<int, bool>();
                            model.SelectedLeaders[leader.Id][team.Id] = selected;
                        }

                    // load existing member
                    foreach (var leader in memberList)
                        foreach (var team in TeamList)
                        {
                            bool selected = garageDefaultSettingByDay.Find(f => f.EntityTypeKey == (int)EntityTypeKey.Member && f.EntityTypeValue == leader.Id && f.GarageTeamId == team.Id) != null;
                            if (!model.SelectedMembers.ContainsKey(leader.Id))
                                model.SelectedMembers[leader.Id] = new Dictionary<int, bool>();
                            model.SelectedMembers[leader.Id][team.Id] = selected;
                        }


                    // load existing cars
                    foreach (var car in carList)
                        foreach (var team in TeamList)
                        {
                            bool selected = garageDefaultSettingByDay.Find(f => f.EntityTypeKey == (int)EntityTypeKey.Car && f.EntityTypeValue == car.CarId.ToString() && f.GarageTeamId == team.Id) != null;
                            if (!model.SelectedCars.ContainsKey(car.CarId))
                                model.SelectedCars[car.CarId] = new Dictionary<int, bool>();
                            model.SelectedCars[car.CarId][team.Id] = selected;

                            if (selected)
                            {
                                var garageDefaultSettingByDayCarPaid = garageDefaultSettingByDay.Where(f => f.EntityTypeKey == (int)EntityTypeKey.Car && f.EntityTypeValue == car.CarId.ToString() && f.GarageTeamId == team.Id).SingleOrDefault();

                                var userpackageDetail = db.UserPackages.Where(u => u.CarId == car.CarId && u.IsActive == true).SingleOrDefault();
                                if (userpackageDetail != null)
                                {
                                    string PaymentStatus = string.Empty;
                                    var paypalAutoPaymentDetail = db.PaypalAutoPayments.Where(p => p.UserPackageID == userpackageDetail.Id && p.ServiceDate == model.ServiceDate).SingleOrDefault();

                                    if (paypalAutoPaymentDetail == null)
                                        PaymentStatus = "UnPaid";
                                    else
                                        PaymentStatus = paypalAutoPaymentDetail.IsPaid ? "Paid" : "UnPaid";

                                    var CarDetail = model.AvailableCars.Where(c => c.Value == garageDefaultSettingByDayCarPaid.EntityTypeValue).SingleOrDefault();
                                    CarDetail.Text += "<br/> <b>Payment Status:</b> " + PaymentStatus;
                                }

                            }
                        }

                    /// Load Car Payment Detail
                    /// 
                    model.CarPayments = new List<CarServicesPayment>();
                    foreach (var car in carList)
                    {
                        CarServicesPayment carPayment = new CarServicesPayment();
                        carPayment.CarId = car.CarId;
                        carPayment.DisplayName = car.DisplayName;
                        carPayment.LicenseNumber = car.LicenseNumber;
                        carPayment.Make = car.Make;
                        carPayment.Color = car.Color;

                        var garageDefaultSettingByDayCarPaid = garageDefaultSettingByDay.Where(f => f.EntityTypeKey == (int)EntityTypeKey.Car && f.EntityTypeValue == car.CarId.ToString()).SingleOrDefault();
                        carPayment.ServiceDayId = garageDefaultSettingByDayCarPaid.Id;

                        var userpackageDetail = db.UserPackages.Where(u => u.CarId == car.CarId && u.IsActive == true).SingleOrDefault();
                        if (userpackageDetail != null)
                        {
                            string PaymentStatus = string.Empty;
                            var paypalAutoPaymentDetail = db.PaypalAutoPayments.Where(p => p.UserPackageID == userpackageDetail.Id && p.ServiceDate == model.ServiceDate).FirstOrDefault();

                            if (paypalAutoPaymentDetail == null)
                                carPayment.IsPaid = false;
                            else
                                carPayment.IsPaid = paypalAutoPaymentDetail.IsPaid ? true : false;
                        }


                        model.CarPayments.Add(carPayment);
                    }

                    /// Load Car With Services
                    /// 

                    model.CarServicesList = new List<CarServices>();
                    foreach (var car in carList)
                        foreach (var team in TeamList)
                        {
                            bool selected = garageDefaultSettingByDay.Find(f => f.EntityTypeKey == (int)EntityTypeKey.Car && f.EntityTypeValue == car.CarId.ToString() && f.GarageTeamId == team.Id) != null;
                            if (selected)
                            {
                                CarServices carService = new CarServices();
                                carService.CarDisplayName = car.DisplayName;
                                carService.CarId = car.CarId;
                                carService.TeamId = team.Id;


                                var pCarId = new SqlParameter();
                                pCarId.ParameterName = "CarId";
                                pCarId.Value = car.CarId;
                                pCarId.DbType = DbType.Int32;

                                var servicesList = db.Database.SqlQuery<GreenPro.Data.Service>(
                                                   "EXEC dbo.GetServicesByCarId @CarId",
                                                   pCarId
                                                   ).ToList();

                                if (servicesList.Count > 0)
                                {
                                    carService.SelectServices = new List<CarServices.SelectService>();
                                    foreach (var service in servicesList)
                                    {
                                        CarServices.SelectService seviceModel = new CarServices.SelectService();
                                        seviceModel.ServiceName = service.Service_Name;
                                        carService.SelectServices.Add(seviceModel);
                                    }
                                }

                                model.CarServicesList.Add(carService);

                            }
                        }

                }
            }
            else if (garageDefaultSettingEitity != null)
            {
                var garageDefaultSettingByDay = garageDefaultSettingEitity.Where(q => q.ServiceDay == model.ServiceDay).ToList();

                if (garageDefaultSettingByDay.Count > 0)
                {
                    // load existing leaders
                    foreach (var leader in LeadersList)
                        foreach (var team in TeamList)
                        {
                            bool selected = garageDefaultSettingByDay.Find(f => f.EntityTypeKey == (int)EntityTypeKey.Leader && f.EntityTypeValue == leader.Id && f.GarageTeamId == team.Id) != null;
                            if (!model.SelectedLeaders.ContainsKey(leader.Id))
                                model.SelectedLeaders[leader.Id] = new Dictionary<int, bool>();
                            model.SelectedLeaders[leader.Id][team.Id] = selected;
                        }

                    // load existing member
                    foreach (var leader in memberList)
                        foreach (var team in TeamList)
                        {
                            bool selected = garageDefaultSettingByDay.Find(f => f.EntityTypeKey == (int)EntityTypeKey.Member && f.EntityTypeValue == leader.Id && f.GarageTeamId == team.Id) != null;
                            if (!model.SelectedMembers.ContainsKey(leader.Id))
                                model.SelectedMembers[leader.Id] = new Dictionary<int, bool>();
                            model.SelectedMembers[leader.Id][team.Id] = selected;
                        }


                    // load existing cars
                    foreach (var car in carList)
                        foreach (var team in TeamList)
                        {
                            bool selected = garageDefaultSettingByDay.Find(f => f.EntityTypeKey == (int)EntityTypeKey.Car && f.EntityTypeValue == car.CarId.ToString() && f.GarageTeamId == team.Id) != null;
                            if (!model.SelectedCars.ContainsKey(car.CarId))
                                model.SelectedCars[car.CarId] = new Dictionary<int, bool>();
                            model.SelectedCars[car.CarId][team.Id] = selected;
                        }



                }

            }

        }


        // GET: GaragesCarDay
        public ActionResult Index(int Id)
        {
            DateTime serviceDate = DateTime.Now.Date.AddDays(1);
            var currentDay = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString();

            ViewData["WorkingDay"] = DateTime.Now.Date.AddDays(1).ToLongDateString();

            var garage = db.Garages.Where(i => i.GarageId == Id).SingleOrDefault();

            var defaultSetting = db.Garage_LeaderSetting.Where(i => i.GarageId == Id);

            var garageCarDaySetting = db.Garage_CarDaySetting.Where(i => i.GarageId == Id && i.ServiceDay == currentDay && i.CarServiceDate == serviceDate).ToList();

            if (garage == null)
                return RedirectToAction("Index");

            DefaultSettingViewModel model = new DefaultSettingViewModel();
            model.ServiceDay = currentDay;
            model.ServiceDate = serviceDate;
            model.PrepareModelData = true;
            PrepareDefaultSettingModel(model, garage, defaultSetting, garageCarDaySetting);


            return View(model);
        }

        [HttpPost, ActionName("Index"), ParameterBasedOnFormName("save-locked", "IsLocked")]
        [FormValueRequired("btnSaveLeaders", "save-locked")]
        public ActionResult Index(DefaultSettingViewModel model, bool IsLocked)
        {
            var garage = db.Garages.Where(i => i.GarageId == model.GarageId).SingleOrDefault();

            DateTime serviceDate = DateTime.Now.Date.AddDays(1);
            var currentDay = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString();

            if (garage == null)
                return RedirectToAction("Index");

            if (garage == null)
                return RedirectToAction("Index");

            var LeadersList = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Name == "Crew Leader")).ToList();
            var TeamList = db.GarageTeams.Where(t => t.GarageId == model.GarageId).ToList();

            // Save Leader Information
            #region Leader Information
            foreach (var team in TeamList)
            {

                string formKey = "restrict_" + team.Id;
                var selectedValue = Request.Form[formKey] != null
                    ? Request.Form[formKey] : string.Empty;

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    //prepare parameters
                    var pEntityTypeValues = new SqlParameter();
                    pEntityTypeValues.ParameterName = "EntityTypeValues";
                    pEntityTypeValues.Value = selectedValue;
                    pEntityTypeValues.DbType = DbType.String;



                    var pGarageTeamId = new SqlParameter();
                    pGarageTeamId.ParameterName = "GarageTeamId";
                    pGarageTeamId.Value = team.Id;
                    pGarageTeamId.DbType = DbType.Int32;

                    var pEntityTypeKey = new SqlParameter();
                    pEntityTypeKey.ParameterName = "EntityTypeKey";
                    pEntityTypeKey.Value = (int)EntityTypeKey.Leader;
                    pEntityTypeKey.DbType = DbType.Int32;


                    var pGarageId = new SqlParameter();
                    pGarageId.ParameterName = "GarageId";
                    pGarageId.Value = model.GarageId;
                    pGarageId.DbType = DbType.Int32;

                    var pServiceDay = new SqlParameter();
                    pServiceDay.ParameterName = "ServiceDay";
                    pServiceDay.Value = model.ServiceDay;
                    pServiceDay.DbType = DbType.String;

                    var pCarServiceDate = new SqlParameter();
                    pCarServiceDate.ParameterName = "CarServiceDate";
                    pCarServiceDate.Value = serviceDate;
                    pCarServiceDate.DbType = DbType.Date;

                    var pIsLocked = new SqlParameter();
                    pIsLocked.ParameterName = "IsLocked";
                    pIsLocked.Value = IsLocked;
                    pIsLocked.DbType = DbType.Boolean;


                    var pIsPaid = new SqlParameter();
                    pIsPaid.ParameterName = "IsPaid";
                    pIsPaid.Value = false;
                    pIsPaid.DbType = DbType.Boolean;

                    //invoke stored procedure EntityTypeKey
                    db.Database.ExecuteSqlCommand(
                       "EXEC [Sproc_InsertOrUpdateGarage_CarDaySetting] @GarageTeamId, @EntityTypeKey, @EntityTypeValues, @GarageId, @ServiceDay, @CarServiceDate, @IsLocked, @IsPaid",
                       pGarageTeamId,
                       pEntityTypeKey,
                       pEntityTypeValues,
                       pGarageId,
                       pServiceDay,
                       pCarServiceDate,
                       pIsLocked,
                       pIsPaid);
                }
            }
            #endregion

            // Save Member Information
            #region Member Information

            foreach (var team in TeamList)
            {

                string formKey = "member_" + team.Id;
                var selectedValue = Request.Form[formKey] != null
                    ? Request.Form[formKey] : string.Empty;

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    //prepare parameters
                    var pEntityTypeValues = new SqlParameter();
                    pEntityTypeValues.ParameterName = "EntityTypeValues";
                    pEntityTypeValues.Value = selectedValue;
                    pEntityTypeValues.DbType = DbType.String;



                    var pGarageTeamId = new SqlParameter();
                    pGarageTeamId.ParameterName = "GarageTeamId";
                    pGarageTeamId.Value = team.Id;
                    pGarageTeamId.DbType = DbType.Int32;

                    var pEntityTypeKey = new SqlParameter();
                    pEntityTypeKey.ParameterName = "EntityTypeKey";
                    pEntityTypeKey.Value = (int)EntityTypeKey.Member;
                    pEntityTypeKey.DbType = DbType.Int32;


                    var pGarageId = new SqlParameter();
                    pGarageId.ParameterName = "GarageId";
                    pGarageId.Value = model.GarageId;
                    pGarageId.DbType = DbType.Int32;

                    var pServiceDay = new SqlParameter();
                    pServiceDay.ParameterName = "ServiceDay";
                    pServiceDay.Value = model.ServiceDay;
                    pServiceDay.DbType = DbType.String;

                    var pCarServiceDate = new SqlParameter();
                    pCarServiceDate.ParameterName = "CarServiceDate";
                    pCarServiceDate.Value = serviceDate;
                    pCarServiceDate.DbType = DbType.Date;

                    var pIsLocked = new SqlParameter();
                    pIsLocked.ParameterName = "IsLocked";
                    pIsLocked.Value = IsLocked;
                    pIsLocked.DbType = DbType.Boolean;

                    var pIsPaid = new SqlParameter();
                    pIsPaid.ParameterName = "IsPaid";
                    pIsPaid.Value = false;
                    pIsPaid.DbType = DbType.Boolean;

                    //invoke stored procedure EntityTypeKey
                    db.Database.ExecuteSqlCommand(
                       "EXEC [Sproc_InsertOrUpdateGarage_CarDaySetting] @GarageTeamId, @EntityTypeKey, @EntityTypeValues, @GarageId, @ServiceDay, @CarServiceDate, @IsLocked, @IsPaid",
                       pGarageTeamId,
                       pEntityTypeKey,
                       pEntityTypeValues,
                       pGarageId,
                       pServiceDay,
                       pCarServiceDate,
                       pIsLocked,
                       pIsPaid);


                }
            }

            #endregion

            // Save Car Information

            #region Car Information

            foreach (var team in TeamList)
            {

                string formKey = "car_" + team.Id;
                var selectedValue = Request.Form[formKey] != null
                    ? Request.Form[formKey] : string.Empty;

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    //prepare parameters
                    var pEntityTypeValues = new SqlParameter();
                    pEntityTypeValues.ParameterName = "EntityTypeValues";
                    pEntityTypeValues.Value = selectedValue;
                    pEntityTypeValues.DbType = DbType.String;



                    var pGarageTeamId = new SqlParameter();
                    pGarageTeamId.ParameterName = "GarageTeamId";
                    pGarageTeamId.Value = team.Id;
                    pGarageTeamId.DbType = DbType.Int32;

                    var pEntityTypeKey = new SqlParameter();
                    pEntityTypeKey.ParameterName = "EntityTypeKey";
                    pEntityTypeKey.Value = (int)EntityTypeKey.Car;
                    pEntityTypeKey.DbType = DbType.Int32;


                    var pGarageId = new SqlParameter();
                    pGarageId.ParameterName = "GarageId";
                    pGarageId.Value = model.GarageId;
                    pGarageId.DbType = DbType.Int32;

                    var pServiceDay = new SqlParameter();
                    pServiceDay.ParameterName = "ServiceDay";
                    pServiceDay.Value = model.ServiceDay;
                    pServiceDay.DbType = DbType.String;

                    var pCarServiceDate = new SqlParameter();
                    pCarServiceDate.ParameterName = "CarServiceDate";
                    pCarServiceDate.Value = serviceDate;
                    pCarServiceDate.DbType = DbType.Date;

                    var pIsLocked = new SqlParameter();
                    pIsLocked.ParameterName = "IsLocked";
                    pIsLocked.Value = IsLocked;
                    pIsLocked.DbType = DbType.Boolean;

                    var pIsPaid = new SqlParameter();
                    pIsPaid.ParameterName = "IsPaid";
                    pIsPaid.Value = false;
                    pIsPaid.DbType = DbType.Boolean;

                    //invoke stored procedure EntityTypeKey
                    db.Database.ExecuteSqlCommand(
                       "EXEC [Sproc_InsertOrUpdateGarage_CarDaySetting] @GarageTeamId, @EntityTypeKey, @EntityTypeValues, @GarageId, @ServiceDay, @CarServiceDate, @IsLocked, @IsPaid",
                       pGarageTeamId,
                       pEntityTypeKey,
                       pEntityTypeValues,
                       pGarageId,
                       pServiceDay,
                       pCarServiceDate,
                       pIsLocked,
                       pIsPaid);
                }
            }

            #endregion

            AddNotification(Models.NotifyType.Success, "Records Successfully Saved", true);

            return RedirectToAction("Index", new { Id = model.GarageId });
        }

        [HttpPost, ActionName("Index")]
        [FormValueRequired("btnAutoPayment")]
        public ActionResult TakePayment(DefaultSettingViewModel model)
        {
            BillGenerator obj = new BillGenerator();

            DateTime serviceDate = DateTime.Now.Date.AddDays(1);
            var currentDay = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString();

            var userSuppliedAuthor = new SqlParameter("@ServiceDate", serviceDate);

            var result = db.Database.SqlQuery<Garage_CarDaySettingPaymentDetailModel>("exec dbo.GetGarage_CarDaySettingPaymentDetail @ServiceDate", userSuppliedAuthor).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    // Skip if payment is already paid (True).
                    if (item.IsPaid)
                        continue;

                    // Other wise take payment from paypal.
                    try
                    {

                        string responsePayment = obj.TakePaymentFromPaypal(item.UserPackageID, item.BillingAggrementID, item.CarServiceDate, item.JobId);
                        string text = "Paypal Call: " + DateTime.Now.ToString();
                        text += Environment.NewLine + Environment.NewLine + "responseFrom Paypal: " + responsePayment;
                        string fileName = item.JobId + "-" + item.UserPackageID + "-" + item.BillingAggrementID + "__" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture) + ".txt";
                        System.IO.File.WriteAllText(Server.MapPath("~/App_Data/" + fileName), text);
                    }
                    catch (Exception ex)
                    {
                        string fileName = "ErrorLog_" + item.JobId + "-" + item.UserPackageID + "-" + item.BillingAggrementID + "__" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture) + ".txt";
                        System.IO.File.WriteAllText(Server.MapPath("~/App_Data/" + fileName), ex.ToString());
                    }

                }
            }
            return RedirectToAction("Index", new { Id = model.GarageId });
        }



        [HttpPost, ActionName("Index")]
        [FormValueRequired(FormValueRequirement.StartsWith, "btnMakePayment-")]
        public ActionResult TakePaymentByCar(DefaultSettingViewModel model)
        {
            int carId = 0;
            
            
            BillGenerator obj = new BillGenerator();

            DateTime serviceDate = DateTime.Now.Date.AddDays(1);
            var currentDay = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString();

            var userSuppliedAuthor = new SqlParameter("@ServiceDate", serviceDate);

            var result = db.Database.SqlQuery<Garage_CarDaySettingPaymentDetailModel>("exec dbo.GetGarage_CarDaySettingPaymentDetail @ServiceDate", userSuppliedAuthor).ToList();


            if (result.Count > 0)
            {
              
                foreach (var item in result)
                {
                    carId = 0;

                    // Skip if payment is already paid (True).
                    if (item.IsPaid)
                        continue;

                    foreach(string key in Request.Form.Keys)
                    {
                        if(key=="btnMakePayment-"+item.CarId)
                            carId=item.CarId;
                    }


                    if(carId>0)
                    {
                        // Other wise take payment from paypal.
                        try
                        {

                            string responsePayment = obj.TakePaymentFromPaypal(item.UserPackageID, item.BillingAggrementID, item.CarServiceDate, item.JobId);
                            string text = "Paypal Call: " + DateTime.Now.ToString();
                            text += Environment.NewLine + Environment.NewLine + "responseFrom Paypal: " + responsePayment;
                            string fileName = item.JobId + "-" + item.UserPackageID + "-" + item.BillingAggrementID + "__" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture) + ".txt";
                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/" + fileName), text);
                        }
                        catch (Exception ex)
                        {
                            string fileName = "ErrorLog_" + item.JobId + "-" + item.UserPackageID + "-" + item.BillingAggrementID + "__" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture) + ".txt";
                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/" + fileName), ex.ToString());
                        }

                        break;
                    }

                }
            }
            return RedirectToAction("Index", new { Id = model.GarageId });
        }


        [HttpPost, ActionName("Index")]
        [FormValueRequired("btnSendNotification")]
        public ActionResult SendNotificationToLeader(DefaultSettingViewModel model)
        {
            var garage = db.Garages.Where(i => i.GarageId == model.GarageId).SingleOrDefault();

            DateTime serviceDate = DateTime.Now.Date.AddDays(1);
            var currentDay = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString();

            if (garage == null)
                return RedirectToAction("Index");

            if (garage == null)
                return RedirectToAction("Index");

            var TeamList = db.GarageTeams.Where(t => t.GarageId == model.GarageId).ToList();

            var result = db.Garage_CarDaySetting.Where(q => q.CarServiceDate == serviceDate
                && q.ServiceDay == currentDay
                && q.GarageId == model.GarageId).ToList();

            if (result.Count > 0)
            {
                foreach (var team in TeamList)
                {
                    // get leaderInformation
                    var leaderInfromation = result.Where(q => q.GarageTeamId == team.Id
                        && q.EntityTypeKey == (int)EntityTypeKey.Leader
                        ).FirstOrDefault();

                    var aspLeaderInfo = db.AspNetUsers.Where(u => u.Id == leaderInfromation.EntityTypeValue).SingleOrDefault();

                    // get leader member information
                    var leaderMemberList = result.Where(q => q.GarageTeamId == team.Id
                        && q.EntityTypeKey == (int)EntityTypeKey.Member
                        ).ToList();

                    string memberHtml = string.Empty;
                    if (leaderMemberList.Count > 0)
                    {
                        memberHtml += "<ul>";
                        foreach (var leaderMember in leaderMemberList)
                        {
                            var memberInfo = db.AspNetUsers.Where(u => u.Id == leaderMember.EntityTypeValue).SingleOrDefault();
                            if (memberInfo != null)
                            {
                                memberHtml += "<li> " + memberInfo.LastName + " " + memberInfo.FirstName + "</li>";
                            }
                        }
                        memberHtml += "</ul>";
                    }



                    // get car information.
                    string carHtml = string.Empty;
                    var carList = result.Where(q => q.GarageTeamId == team.Id
                        && q.EntityTypeKey == (int)EntityTypeKey.Car
                        ).ToList();

                    if (carList.Count > 0)
                    {
                        carHtml += "<table>";
                        carHtml += "<tr>";
                        carHtml += "<td>Display Name</td>";
                        carHtml += "<td>License Number </td>";
                        carHtml += "<td>Color</td>";
                        carHtml += "<td>Make</td>";
                        carHtml += "<td>PurchaseYear</td>";
                        carHtml += "</tr>";
                        foreach (var car in carList)
                        {
                            int carid = Convert.ToInt32(car.EntityTypeValue);
                            var carInfo = db.CarUsers.Where(q => q.CarId == carid).FirstOrDefault();
                            if (carInfo != null)
                            {
                                carHtml += "<tr>";

                                carHtml += "<td>" + carInfo.DisplayName + "</td>";
                                carHtml += "<td>" + carInfo.LicenseNumber + "</td>";
                                carHtml += "<td>" + carInfo.Color + "</td>";
                                carHtml += "<td>" + carInfo.Make + "</td>";
                                carHtml += "<td>" + carInfo.PurchaseYear + "</td>";

                                carHtml += "</tr>";
                            }
                        }

                        carHtml += "</table>";
                    }

                    _workflowMessageService.SendCrewLeaderNotification(aspLeaderInfo.FirstName + " " + aspLeaderInfo.LastName, aspLeaderInfo.Email, Convert.ToString(carList.Count), serviceDate.ToShortDateString()
                        , memberHtml
                        , carHtml);
                }
            }


            SuccessNotification("Mail Sent Successfully.");
            return RedirectToAction("Index", new { Id = model.GarageId });
        }
    }
}