using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GreenPro.Data;

namespace GreenPro.AdminInterface.Controllers
{
    public class LogDetailCarSidesController : BaseController
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: LogDetailCarSides
        public ActionResult Index()
        {
            return View(db.LogDetailCarSides.ToList());
        }

        // GET: LogDetailCarSides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogDetailCarSide logDetailCarSide = db.LogDetailCarSides.Find(id);
            if (logDetailCarSide == null)
            {
                return HttpNotFound();
            }
            return View(logDetailCarSide);
        }

        // GET: LogDetailCarSides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogDetailCarSides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] LogDetailCarSide logDetailCarSide)
        {
            if (ModelState.IsValid)
            {
                db.LogDetailCarSides.Add(logDetailCarSide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logDetailCarSide);
        }

        // GET: LogDetailCarSides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogDetailCarSide logDetailCarSide = db.LogDetailCarSides.Find(id);
            if (logDetailCarSide == null)
            {
                return HttpNotFound();
            }
            return View(logDetailCarSide);
        }

        // POST: LogDetailCarSides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] LogDetailCarSide logDetailCarSide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logDetailCarSide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logDetailCarSide);
        }

        // GET: LogDetailCarSides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogDetailCarSide logDetailCarSide = db.LogDetailCarSides.Find(id);
            if (logDetailCarSide == null)
            {
                return HttpNotFound();
            }
            return View(logDetailCarSide);
        }

        // POST: LogDetailCarSides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogDetailCarSide logDetailCarSide = db.LogDetailCarSides.Find(id);
            db.LogDetailCarSides.Remove(logDetailCarSide);
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
