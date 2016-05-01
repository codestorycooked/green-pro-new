using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreenPro.Data;

namespace GreenPro.AdminInterface.Controllers
{
    public class UnAssignedCarsController : BaseController
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: UnAssignedCars
        public ActionResult Index()
        {
            var unAssignedCars = db.UnAssignedCars.Include(u => u.Garage).Include(u => u.UserPackage);
            return View(unAssignedCars.ToList());
        }

        // GET: UnAssignedCars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnAssignedCar unAssignedCar = db.UnAssignedCars.Find(id);
            if (unAssignedCar == null)
            {
                return HttpNotFound();
            }
            return View(unAssignedCar);
        }

        // GET: UnAssignedCars/Create
        public ActionResult Create()
        {
            ViewBag.GarageID = new SelectList(db.Garages, "GarageId", "Garage_Name");
            ViewBag.PackageId = new SelectList(db.UserPackages, "Id", "UserId");
            return View();
        }

        // POST: UnAssignedCars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GarageID,PackageId,DateTime")] UnAssignedCar unAssignedCar)
        {
            if (ModelState.IsValid)
            {
                db.UnAssignedCars.Add(unAssignedCar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GarageID = new SelectList(db.Garages, "GarageId", "Garage_Name", unAssignedCar.GarageID);
            ViewBag.PackageId = new SelectList(db.UserPackages, "Id", "UserId", unAssignedCar.PackageId);
            return View(unAssignedCar);
        }

        // GET: UnAssignedCars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnAssignedCar unAssignedCar = db.UnAssignedCars.Find(id);
            if (unAssignedCar == null)
            {
                return HttpNotFound();
            }
            ViewBag.GarageID = new SelectList(db.Garages, "GarageId", "Garage_Name", unAssignedCar.GarageID);
            ViewBag.PackageId = new SelectList(db.UserPackages, "Id", "UserId", unAssignedCar.PackageId);
            return View(unAssignedCar);
        }

        // POST: UnAssignedCars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GarageID,PackageId,DateTime")] UnAssignedCar unAssignedCar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unAssignedCar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GarageID = new SelectList(db.Garages, "GarageId", "Garage_Name", unAssignedCar.GarageID);
            ViewBag.PackageId = new SelectList(db.UserPackages, "Id", "UserId", unAssignedCar.PackageId);
            return View(unAssignedCar);
        }

        // GET: UnAssignedCars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnAssignedCar unAssignedCar = db.UnAssignedCars.Find(id);
            if (unAssignedCar == null)
            {
                return HttpNotFound();
            }
            return View(unAssignedCar);
        }

        // POST: UnAssignedCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnAssignedCar unAssignedCar = db.UnAssignedCars.Find(id);
            db.UnAssignedCars.Remove(unAssignedCar);
            db.SaveChanges();
            return RedirectToAction("Index");
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
