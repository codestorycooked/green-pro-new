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
namespace GreenPro.WebClient.Views
{
    public class UserTransactionsController : Controller
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: UserTransactions
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var userTransactions = db.UserTransactions.Include(u => u.UserPackage).Where(a => a.Userid == userid).OrderByDescending(o=>o.Id);
            return View(userTransactions.ToList());
        }

        // GET: UserTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTransaction userTransaction = db.UserTransactions.Find(id);
            if (userTransaction == null)
            {
                return HttpNotFound();
            }
            return View(userTransaction);
        }

        // GET: UserTransactions/Create
        public ActionResult Create()
        {
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "Package_Name");
            return View();
        }

        // POST: UserTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TransactionDate,Amount,PackageId,PaypalId,Details")] UserTransaction userTransaction)
        {
            if (ModelState.IsValid)
            {
                db.UserTransactions.Add(userTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "Package_Name", userTransaction.PackageId);
            return View(userTransaction);
        }

        // GET: UserTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTransaction userTransaction = db.UserTransactions.Find(id);
            if (userTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "Package_Name", userTransaction.PackageId);
            return View(userTransaction);
        }

        // POST: UserTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TransactionDate,Amount,PackageId,PaypalId,Details")] UserTransaction userTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "Package_Name", userTransaction.PackageId);
            return View(userTransaction);
        }

        // GET: UserTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTransaction userTransaction = db.UserTransactions.Find(id);
            if (userTransaction == null)
            {
                return HttpNotFound();
            }
            return View(userTransaction);
        }

        // POST: UserTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTransaction userTransaction = db.UserTransactions.Find(id);
            db.UserTransactions.Remove(userTransaction);
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
