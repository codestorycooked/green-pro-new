using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Geocoding;
using Geocoding.Google;
using GreenPro.Data;
using GreenPro.AdminInterface.Models;
using GreenPro.AdminInterface.Helper;

namespace GreenPro.AdminInterface.Controllers
{
     [Authorize(Roles = "Admin")]
    public class GaragesController : BaseController
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: Garages
        public ActionResult Index()
        {
            return View(db.Garages.Where(a => a.IsActive == true).ToList());
        }

        public ActionResult ApproveGarages()
        {

            return View(db.Garages.Where(a => a.IsActive == false).ToList());
        }
        [HttpPost]
        public ActionResult ApproveGarages(int? GarageID)
        {
            if (GarageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garage garage = db.Garages.Find(GarageID);
            if (garage == null)
            {
                return HttpNotFound();
            }
            else
            {
                garage.IsActive = true;
                db.Entry(garage).State = EntityState.Modified;
                db.SaveChanges();


            }
            return View("ApproveGarages");
        }
        // GET: Garages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garage garage = db.Garages.Find(id);
            if (garage == null)
            {
                return HttpNotFound();
            }
            return View(garage);
        }

        // GET: Garages/Create
        public ActionResult Create()
        {
            GarageViewModel model = new GarageViewModel();

            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");                       
            model.AvailableServiceDays = ListHelper.GetDayNameList();
            return View(model);
        }


        // POST: Garages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GarageViewModel model)
        {
           
            model.Country = "US";
            model.CreatedDt = DateTime.Now.Date;
            model.CreatedBy = "ADmin";
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyA3CNMI-_JAV9-dWIctroZQTuUwjZygT3A" };
            IEnumerable<Address> addresses = geocoder.Geocode(model.Garage_Address);
            model.Latitute = addresses.First().Coordinates.Latitude;
            model.Longitude = addresses.First().Coordinates.Longitude;

            ModelState.Remove("State");
            ModelState.Remove("City");
            ModelState.Remove("Country");
            if (ModelState.IsValid)
            {
                Garage entity = new Garage();
                entity.Garage_Name = model.Garage_Name;
                entity.Contact_Person = model.Contact_Person;
                entity.Garage_Address = model.Garage_Address;
                entity.Phone_Number = model.Phone_Number;
                entity.Email = model.Email;
                entity.IsActive = model.IsActive.HasValue ? model.IsActive.Value : false;
                entity.Garage_Address = model.Garage_Address;
                entity.City = model.City;
                entity.State = model.State;
                entity.Pincode = model.Pincode;
                entity.OpenTime = model.OpenTime;
                entity.CloseTime = model.CloseTime;
                //entity.ServiceDays = model.ServiceDays;

                entity.ServiceDays = string.Join(",",model.ServiceDays);

                entity.Country = "US";
                entity.CreatedDt = DateTime.Now.Date;
                entity.CreatedBy = "Admin";
                entity.Latitute = model.Latitute;
                entity.Longitude = model.Longitude;

                try
                {
                    db.Garages.Add(entity);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                AddNotification(Models.NotifyType.Success, "Records Successfully Saved.", true);
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(db.States, "Id", "StateName", model.State);
            ViewBag.CityId = new SelectList(db.Cities.Where(b => b.StateID == model.State), "Id", "CityName", model.City);
            model.AvailableServiceDays = ListHelper.GetDayNameList();
            return View(model);
        }

        // GET: Garages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Garage entity = db.Garages.Find(id);

            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName", entity.State);
            ViewBag.CityId = new SelectList(db.Cities.Where(b => b.StateID == entity.State), "Id", "CityName", entity.City);

            GarageViewModel model = new GarageViewModel();
            model.GarageId = entity.GarageId;
            model.Garage_Name = entity.Garage_Name;
            model.Contact_Person = entity.Contact_Person;
            model.Email = entity.Email;
            model.Phone_Number = entity.Phone_Number;
            model.IsActive = entity.IsActive;
            model.Garage_Address = entity.Garage_Address;
            model.City = entity.City;
            model.State = entity.State;
            model.Pincode = entity.Pincode;
            model.OpenTime = entity.OpenTime;
            model.CloseTime = entity.CloseTime;
            model.ServiceDays = entity.ServiceDays.Split(',');
            model.AvailableServiceDays = ListHelper.GetDayNameList();


            return View(model);
        }

        // POST: Garages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GarageViewModel model)
        {
            bool findLatLong = false;
            Garage entity = db.Garages.Find(model.GarageId);

            if (entity == null)
            {
                return HttpNotFound();
            }

            ModelState.Remove("State");
            ModelState.Remove("City");
            ModelState.Remove("Country");
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName", model.State);
            ViewBag.CityId = new SelectList(db.Cities.Where(b => b.StateID == model.State), "Id", "CityName", model.City);

            try
            {
                IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyA3CNMI-_JAV9-dWIctroZQTuUwjZygT3A" };
                IEnumerable<Address> addresses = geocoder.Geocode(model.Garage_Address);
                model.Latitute = addresses.First().Coordinates.Latitude;
                model.Longitude = addresses.First().Coordinates.Longitude;
                findLatLong = true;
            }
            catch
            {
                findLatLong = false;
            }
            
            model.CreatedDt = DateTime.Now.Date;
            model.CreatedBy = User.Identity.Name;
            model.Country = "US";

            if (ModelState.IsValid)
            {
                
                entity.Garage_Name = model.Garage_Name;
                entity.Contact_Person = model.Contact_Person;
                entity.Garage_Address = model.Garage_Address;
                entity.Phone_Number = model.Phone_Number;
                entity.Email = model.Email;
                entity.IsActive = model.IsActive.HasValue ? model.IsActive.Value : false;
                entity.Garage_Address = model.Garage_Address;
                entity.City = model.City;
                entity.State = model.State;
                entity.Pincode = model.Pincode;
                entity.OpenTime = model.OpenTime;
                entity.CloseTime = model.CloseTime;
                //entity.ServiceDays = model.ServiceDays; 
                entity.ServiceDays = string.Join(",", model.ServiceDays);

                if (findLatLong)
                {
                    entity.Latitute = model.Latitute;
                    entity.Longitude = model.Longitude;
                }

                try
                {
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                AddNotification(Models.NotifyType.Success, "Records Successfully Updated.", true);
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(db.States, "Id", "StateName", model.State);
            ViewBag.CityId = new SelectList(db.Cities.Where(b => b.StateID == model.State), "Id", "CityName", model.City);
            model.AvailableServiceDays = ListHelper.GetDayNameList();
            return View(model);
        }

        // GET: Garages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garage garage = db.Garages.Find(id);
            if (garage == null)
            {
                return HttpNotFound();
            }
            return View(garage);
        }

        // POST: Garages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Garage garage = db.Garages.Find(id);
            db.Garages.Remove(garage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public JsonResult Citylist(string id)
        {
            var stateId = Convert.ToInt32(id);
            GreenProDbEntities db = new GreenProDbEntities();
            var cities = db.Cities.Where(a => a.StateID.Value == stateId).ToList();
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            foreach (var city in cities)
            {
                selectListItemList.Add(new SelectListItem { Text = city.CityName, Value = city.Id.ToString() });
            }
            return Json(selectListItemList);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
