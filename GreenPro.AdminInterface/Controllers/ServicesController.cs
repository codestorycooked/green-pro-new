using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GreenPro.Data;

namespace GreenPro.AdminInterface.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServicesController : BaseController
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: Services
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Data.Service service)
        {
            ModelState.Remove("CreateDt");
            ModelState.Remove("CreatedBy");
            if (ModelState.IsValid)
            {
                service.CreatedBy = "Admin";
                service.CreateDt = DateTime.Now.Date;                
                db.Services.Add(service);
                db.SaveChanges();

                AddNotification(Models.NotifyType.Success, "Records Successfully Saved.", true);

                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Data.Service service)
        {
            ModelState.Remove("CreateDt");
            ModelState.Remove("CreatedBy");
            if (ModelState.IsValid)
            {
                service.CreatedBy = "Admin";
                service.CreateDt = DateTime.Now.Date;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();

                AddNotification(Models.NotifyType.Success, "Records Successfully Updated.", true);

                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Data.Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
