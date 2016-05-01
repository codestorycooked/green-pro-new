using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreenPro.AdminInterface.Models;
using GreenPro.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GreenPro.AdminInterface.Controllers
{
    public class AspNetUsersController : BaseController
    {
        private GreenProDbEntities db = new GreenProDbEntities();
        public AspNetUsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: AspNetUsers
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.City1).Include(a => a.State1);
            return View(aspNetUsers.ToList());
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            ViewBag.City = new SelectList(db.Cities, "Id", "CityName");
            ViewBag.State = new SelectList(db.States, "Id", "StateName");
            return View();
        }
       
        // POST: AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AspNetUser aspNetUser)
        {

            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUser);
                var adminUSer = new ApplicationUser()
                {
                    UserName = aspNetUser.Email,
                    FirstName = aspNetUser.FirstName,
                    LastName = aspNetUser.LastName,
                    DateofBirth = DateTime.Now.Date,
                    Address = aspNetUser.Address,
                    State = 1,
                    City = 1,
                    Pincode = aspNetUser.Pincode,
                    Email = aspNetUser.Email
                };
                var result = UserManager.Create(adminUSer, "kunals");
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.City = new SelectList(db.Cities, "Id", "CityName", aspNetUser.City);
            ViewBag.State = new SelectList(db.States, "Id", "StateName", aspNetUser.State);
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.City = new SelectList(db.Cities, "Id", "CityName", aspNetUser.City);
            ViewBag.State = new SelectList(db.States, "Id", "StateName", aspNetUser.State);
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,DateofBirth,Address,State,City,Pincode,Balance")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.City = new SelectList(db.Cities, "Id", "CityName", aspNetUser.City);
            ViewBag.State = new SelectList(db.States, "Id", "StateName", aspNetUser.State);
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
