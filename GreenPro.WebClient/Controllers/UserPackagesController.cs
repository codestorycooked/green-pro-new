using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreenPro.Data;
using Microsoft.AspNet.Identity;

namespace GreenPro.WebClient.Controllers
{
    public class UserPackagesController : Controller
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: UserPackages
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var userPackages = db.UserPackages.Include(u => u.AspNetUser).Include(u => u.CarUser).Include(u => u.Package).Where(a=>a.UserId==userid && a.PaymentRecieved==true);
            return View(userPackages.ToList());
        }

        // GET: UserPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPackage userPackage = db.UserPackages.Find(id);
            if (userPackage == null)
            {
                return HttpNotFound();
            }
            return View(userPackage);
        }
        
        // GET: UserPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPackage userPackage = db.UserPackages.Find(id);
            if (userPackage == null)
            {
                return HttpNotFound();
            }
            return View(userPackage);
        }

        // POST: UserPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPackage userPackage = db.UserPackages.Find(id);
            db.UserPackages.Remove(userPackage);
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
