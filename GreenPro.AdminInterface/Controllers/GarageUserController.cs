//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using GreenPro.Data;

//namespace GreenPro.AdminInterface.Controllers
//{
//    public class GarageUserController : Controller
//    {
//        private GreenProDbEntities db = new GreenProDbEntities();

//        // GET: /GarageUser/
//        public ActionResult Index()
//        {
//            var garageusers = db.GarageUsers.Include(g => g.AspNetUser).Include(g => g.Garage);
//            return View(garageusers.ToList());
//        }

//        // GET: /GarageUser/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            GarageUser garageuser = db.GarageUsers.Find(id);
//            if (garageuser == null)
//            {
//                return HttpNotFound();
//            }
//            return View(garageuser);
//        }

//        // GET: /GarageUser/Create
//        public ActionResult Create()
//        {
//            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
//            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "Garage_Name");
//            return View();
//        }

//        // POST: /GarageUser/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include="Id,GarageId,UserId")] GarageUser garageuser)
//        {
//            if (ModelState.IsValid)
//            {
//                db.GarageUsers.Add(garageuser);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", garageuser.UserId);
//            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "Garage_Name", garageuser.GarageId);
//            return View(garageuser);
//        }

//        // GET: /GarageUser/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            GarageUser garageuser = db.GarageUsers.Find(id);
//            if (garageuser == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", garageuser.UserId);
//            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "Garage_Name", garageuser.GarageId);
//            return View(garageuser);
//        }

//        // POST: /GarageUser/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include="Id,GarageId,UserId")] GarageUser garageuser)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(garageuser).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", garageuser.UserId);
//            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "Garage_Name", garageuser.GarageId);
//            return View(garageuser);
//        }

//        // GET: /GarageUser/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            GarageUser garageuser = db.GarageUsers.Find(id);
//            if (garageuser == null)
//            {
//                return HttpNotFound();
//            }
//            return View(garageuser);
//        }

//        // POST: /GarageUser/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            GarageUser garageuser = db.GarageUsers.Find(id);
//            db.GarageUsers.Remove(garageuser);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
