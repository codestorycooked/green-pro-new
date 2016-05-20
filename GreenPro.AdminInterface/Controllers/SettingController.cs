using GreenPro.AdminInterface.Helper;
using GreenPro.AdminInterface.ViewModels;
using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using GreenPro.Data.Models;


namespace GreenPro.AdminInterface.Controllers
{
    public class SettingController : BaseController
    {
        private GreenProDbEntities db;

        public SettingController()
        {
            db = new GreenProDbEntities();
        }


        #region GarageTeam
        public ActionResult TeamList()
        {
            IList<GarageTeamModel> teammodelList = new List<GarageTeamModel>();
            var garageTeams = db.GarageTeams.ToList();
            GarageTeamModel model;
            foreach (var item in garageTeams)
            {
                var garage = db.Garages.Where(i => i.GarageId == item.GarageId).SingleOrDefault();

                model = new GarageTeamModel();
                model.Id = item.Id;
                model.Title = item.Title;
                if (garage != null)
                    model.GarageName = garage.Garage_Name;
                
                model.GarageId = item.GarageId;
                model.Active = item.Active;
                model.CreatedOn = item.CreatedOn;
                teammodelList.Add(model);

            }
            return View(teammodelList);
        }

        public ActionResult CreateOrEditTeam(int? id)
        {
            GarageTeamModel model = new GarageTeamModel();

            var GarageList = db.Garages.OrderBy(o => o.Garage_Name).ToList();

            if (id.HasValue)
            {
                GarageTeam entity = db.GarageTeams.Where(i => i.Id == id.Value).SingleOrDefault();
                if (entity != null)
                {
                    var garage = db.Garages.Where(i => i.GarageId == entity.GarageId).SingleOrDefault();

                    model.Id = entity.Id;
                    model.Title = entity.Title;
                    model.GarageId = entity.GarageId;
                    if (garage!=null)
                        model.GarageName = garage.Garage_Name;
                    model.Active = entity.Active;
                    model.CreatedOn = entity.CreatedOn;
                }
            }
            foreach (var garage in GarageList)
            {
                model.AvailableGarages.Add(new SelectListItem() { Text = garage.Garage_Name, Value = garage.GarageId.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOrEditTeam(GarageTeamModel model)
        {
            if (ModelState.IsValid)
            {
                
                if (model.Id <= 0)
                {
                    GarageTeam entity = new GarageTeam();
                    entity.Title = model.Title;
                    entity.GarageId = model.GarageId;
                    entity.CreatedOn = DateTime.Now;
                    entity.Active = model.Active;
                    db.GarageTeams.Add(entity);
                    db.SaveChanges();
                }
                else
                {
                    GarageTeam entity = db.GarageTeams.Where(i => i.Id==model.Id).SingleOrDefault();
                    if(entity!=null)
                    {
                        entity.Title = model.Title;
                        entity.GarageId = model.GarageId;                        
                        entity.Active = model.Active;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("TeamList");
            }

            var GarageList = db.Garages.OrderBy(o => o.Garage_Name).ToList();
            foreach (var garage in GarageList)
            {
                model.AvailableGarages.Add(new SelectListItem() { Text = garage.Garage_Name, Value = garage.GarageId.ToString() });
            }

            return View(model);
        }


        #endregion

        // GET: Setting
        public ActionResult Index()
        {
            var garageList=db.Garages.Where(a => a.IsActive == true).ToList();

            return View(garageList);
        }

        protected void PrepareDefaultSettingModel(DefaultSettingViewModel model, Garage garage, IQueryable<Garage_LeaderSetting> garageDefaultSettingEitity, bool loadDefaultValue=true)
        {
            string garageServiesDays = garage.ServiceDays;
            var garageServiesDaysArray = garageServiesDays.Split(',');
            model.GarageName = garage.Garage_Name;
            model.PrepareModelData = true;
            if (loadDefaultValue)
            {
                model.ServiceDay = garageServiesDaysArray[0];

                
            }

            foreach (string day in garageServiesDaysArray)
            {
                model.AvailableServiceDays.Add(new SelectListItem()
                {
                    Text = day,
                    Value = day
                });
            }

            model.GarageId = garage.GarageId;

            // Teams
            var TeamList = db.GarageTeams.Where(t => t.GarageId == model.GarageId).ToList();
            foreach (var team in TeamList)
            {
                model.AvailableTeams.Add(new SelectListItem()
                {
                    Text = team.Title,
                    Value = team.Id.ToString()
                });
            }

            if (TeamList.Count <= 0)
            {
                ErrorNotification("Team not available.");
                model.PrepareModelData = false;
            }

            // Crew Leader
            // Crew Leader
            //var LeadersList = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Name == "Crew Leader")
            //                    && u.WorkerGarages.Any(w => w.GarageID == model.GarageId && w.IsLeader == true)).ToList();

            var LeadersList = (from u in db.AspNetUsers
                              join w in db.WorkerGarages
                                                      on u.Id equals w.CrewLeaderId
                             where w.IsLeader==true && u.AspNetRoles.Any(r => r.Name == "Crew Leader")
                             && w.GarageID==model.GarageId
                             select u).ToList();

            if (LeadersList.Count <= 0)
            {
                ErrorNotification("Leaders not available.");
                model.PrepareModelData = false;
            }

            foreach (var leader in LeadersList)
            {
                model.AvailableLeaders.Add(new SelectListItem()
                {
                    Text = leader.FirstName + " " + leader.LastName+"<br>"+leader.Email,
                    Value = leader.Id
                });


            }

            //foreach (var leader in LeadersList)
            //{
            //    model.AvailableLeaders.Add(new SelectListItem()
            //    {
            //        Text = leader.FirstName + " " + leader.LastName,
            //        Value = leader.Id
            //    });


            //}
            

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
                ErrorNotification("Members not available.");
                model.PrepareModelData = false;
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

            /*
                select c.CarId,c.DisplayName from CarUsers c inner join [dbo].[UserPackages] u 
                on c.CarId=u.CarId
                inner join dbo.Packages p
                on u.PackageId=p.PackageId
                where c.GarageId=3
             */

            var garageList = db.Garages.Where(a => a.ServiceDays == model.ServiceDay).ToList();
            var carList = (
                from c in db.CarUsers
                join
                    up in db.UserPackages
                    on c.CarId equals up.CarId
                join p in db.Packages
                on up.PackageId equals p.PackageId
                where c.GarageId == model.GarageId && up.PaymentRecieved == true && up.IsActive==true && up.ServiceDay==model.ServiceDay
                select new { CarId = c.CarId, DisplayName = c.DisplayName, LicenseNumber=c.LicenseNumber, c.Make,c.Color }).ToList();

            if (carList.Count <= 0)
            {
                //ErrorNotification("Cars not available.");
                //model.PrepareModelData = false;
            }

            // p in db.Packages
            //where u.PackageId == p.PackageId && u.CarId == c.CarId && c.GarageId == model.GarageId
            //select new { CarId = c.CarId, DisplayName = c.DisplayName }).ToList();

            string Description = string.Empty;
            foreach (var car in carList)
            {
                   Description = string.Empty;
                
                    Description = car.DisplayName;

               if(!string.IsNullOrEmpty(Description))
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


            if (garageDefaultSettingEitity != null)
            {
                var garageDefaultSettingByDay = garageDefaultSettingEitity.Where(q => q.ServiceDay == model.ServiceDay).ToList();

                if (garageDefaultSettingByDay.Count > 0)
                {
                    // load existing leaders
                    foreach (var leader in LeadersList)
                        foreach (var team in TeamList)
                        {
                            bool selected = garageDefaultSettingByDay.Find(f=>f.EntityTypeKey==(int)EntityTypeKey.Leader && f.EntityTypeValue==leader.Id && f.GarageTeamId==team.Id)!=null;
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


        public ActionResult DefaultSetting(int Id)
        {
            var garage = db.Garages.Where(i => i.GarageId == Id).SingleOrDefault();

            var defaultSetting = db.Garage_LeaderSetting.Where(i => i.GarageId == Id);

            if (garage == null)
                return RedirectToAction("Index");

            DefaultSettingViewModel model = new DefaultSettingViewModel();
            model.PrepareModelData = true;
            PrepareDefaultSettingModel(model, garage, defaultSetting);

            if (model.PrepareModelData==false)
                return RedirectToAction("Index");
            

            return View(model);
        }

        [HttpPost, ActionName("DefaultSetting")]
        [FormValueRequired("btnSaveLeaders")]
        public ActionResult SaveLeaders(DefaultSettingViewModel model)
        {
            var garage = db.Garages.Where(i => i.GarageId == model.GarageId).SingleOrDefault();

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

                    //invoke stored procedure EntityTypeKey
                     db.Database.ExecuteSqlCommand(
                        "EXEC [Sproc_InsertOrUpdateLeaderSetting] @GarageTeamId, @EntityTypeKey, @EntityTypeValues, @GarageId, @ServiceDay",
                        pGarageTeamId,
                        pEntityTypeKey,
                        pEntityTypeValues,
                        pGarageId,
                        pServiceDay);
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

                    //invoke stored procedure EntityTypeKey
                    db.Database.ExecuteSqlCommand(
                       "EXEC [Sproc_InsertOrUpdateLeaderSetting] @GarageTeamId, @EntityTypeKey, @EntityTypeValues, @GarageId, @ServiceDay",
                       pGarageTeamId,
                       pEntityTypeKey,
                       pEntityTypeValues,
                       pGarageId,
                       pServiceDay);
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

                    //invoke stored procedure EntityTypeKey
                    db.Database.ExecuteSqlCommand(
                       "EXEC [Sproc_InsertOrUpdateLeaderSetting] @GarageTeamId, @EntityTypeKey, @EntityTypeValues, @GarageId, @ServiceDay",
                       pGarageTeamId,
                       pEntityTypeKey,
                       pEntityTypeValues,
                       pGarageId,
                       pServiceDay);
                }
            }

            #endregion

            

             return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DefaultSetting")]
        [FormValueRequired("btnloadSetting")]
        public ActionResult LoadSettingByServiceDay(DefaultSettingViewModel model)
        {
            var garage = db.Garages.Where(i => i.GarageId == model.GarageId).SingleOrDefault();

            
            if (garage == null)
                return RedirectToAction("Index");

            var defaultSetting = db.Garage_LeaderSetting.Where(i => i.GarageId == model.GarageId);
            model.PrepareModelData = true;
            PrepareDefaultSettingModel(model, garage, defaultSetting,false);
            if (model.PrepareModelData == false)
                return RedirectToAction("Index");

            return View(model);

        }
    }
}