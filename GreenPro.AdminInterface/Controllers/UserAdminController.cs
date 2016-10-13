using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GreenPro.AdminInterface.Models;
using GreenPro.Data;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GreenPro.AdminInterface.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersAdminController : BaseController
    {

        private GreenProDbEntities _db;

        public UsersAdminController()
        {
            _db = new GreenProDbEntities();
        }



        public ActionResult Landing()
        {

            return View();
        }
        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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

        //
        // GET: /Users/
        public ActionResult Index()
        {
            //var userList = _db.AspNetUsers.Where(u => u.IsDelete == false).ToList();           

            var userList = _db.AspNetUsers.Where(u => u.IsDelete == false && u.AspNetRoles.Any(r => r.Name == "Users")).ToList();

            return View(userList);
        }

        public ActionResult GaragesEmployee()
        {
            var userList = _db.AspNetUsers.Where(u => u.IsDelete == false && u.AspNetRoles.Any(r => r.Name == "Crew Leader" || r.Name == "Crew Member" || r.Name == "Admin")).ToList();
            return View(userList);
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);
            GreenProDbEntities db = new GreenProDbEntities();

            ViewBag.Garages = db.WorkerGarages.Where(a => a.CrewLeaderId == user.Id).ToList();
            return View(user);
        }

        //
        // GET: /Users/Create
        public ActionResult Create()
        {
            GreenProDbEntities db = new GreenProDbEntities();

            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.Garages = db.Garages.ToList();
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string[] garages)
        {

            GreenProDbEntities db = new GreenProDbEntities();
            //Find Role Name
            var rolename = db.AspNetRoles.FirstOrDefault(a => a.Id == userViewModel.RoleId);
            //If admin Garage Check

            ModelState.Remove("Email");
            userViewModel.Email = userViewModel.UserName;
            if (rolename != null && (garages == null && rolename.Name != "Admin"))
            {
                ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
                ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
                ViewBag.Garages = db.Garages.ToList();
                ViewBag.ErrorMessage = "Please select garages for user";
                return View();
            }
            else if (ModelState.IsValid)
            {
                //Create User 
                var user = new ApplicationUser() { Email = userViewModel.UserName, UserName = userViewModel.UserName, FirstName = userViewModel.FirstName, LastName = userViewModel.LastName, DateofBirth = userViewModel.DateofBirth, Address = userViewModel.Address, State = userViewModel.StateId, City = userViewModel.CityId, Pincode = userViewModel.Pincode, EmailConfirmed = true };
                var isUSer = UserManager.FindByEmail(user.Email);
                if (isUSer == null)
                {


                    var adminresult = UserManager.Create(user, userViewModel.Password);
                    if (adminresult.Succeeded)
                    {

                        var userdetails = db.AspNetUsers.FirstOrDefault(a => a.Id == user.Id);
                        if (userdetails != null)
                        {
                            if (rolename != null)
                            {
                                var result = UserManager.AddToRole(userdetails.Id, rolename.Name);
                                if (result.Succeeded)
                                {
                                    if (rolename.Name != "Admin")
                                    {
                                        //add user to garagaes
                                        if (garages != null)
                                            foreach (var item in garages)
                                            {
                                                bool isLeader = rolename.Name == "Crew Leader";
                                                WorkerGarage worker = new WorkerGarage { GarageID = Convert.ToInt32(item), CrewLeaderId = user.Id, IsLeader = isLeader };
                                                db.WorkerGarages.Add(worker);
                                            }
                                        await db.SaveChangesAsync();
                                    }
                                }
                                else
                                {
                                    var logins = user.Logins;
                                    foreach (var item in logins.ToList())
                                    {
                                        await UserManager.RemoveLoginAsync(item.UserId, new UserLoginInfo(item.LoginProvider, item.ProviderKey));

                                    }
                                }
                            }
                        }
                        AddNotification(Models.NotifyType.Success, "Records Successfully Saved", true);
                        return RedirectToAction("Index", "UsersAdmin");
                    }

                }
                else
                {
                    ViewBag.ErrorMessage = "User Already Exist's !";
                    ErrorNotification("User Already Exist's !");
                }
            }

            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.Garages = db.Garages.ToList();

            return View();

        }


        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            GreenProDbEntities db = new GreenProDbEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");

            EditUserViewModel model = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateofBirth = user.DateofBirth,
                DateofBirthStr = user.DateofBirth.ToString(),
                Address = user.Address,
                StateId = user.State,
                CityId = user.City,
                PhoneNumber = user.PhoneNumber,
                Pincode = user.Pincode,

                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            };


            return View(model);
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUser, params string[] selectedRole)
        {
            GreenProDbEntities db = new GreenProDbEntities();
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");


            var userRoles = await UserManager.GetRolesAsync(editUser.Id);
            selectedRole = selectedRole ?? new string[] { };

            editUser.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
            {
                Selected = userRoles.Contains(x.Name),
                Text = x.Name,
                Value = x.Name
            });

            ModelState.Remove("Email");
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.UserName;
                user.Email = editUser.UserName;

                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.Address = editUser.Address;
                user.City = editUser.CityId;
                user.State = editUser.StateId;
                user.Pincode = editUser.Pincode;
                user.PhoneNumber = editUser.PhoneNumber;
                user.DateofBirth = editUser.DateofBirth;


                var updateResult = UserManager.Update(user);
                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError("", updateResult.Errors.First());
                    return View(editUser);
                }



                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(editUser);
                }

                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(editUser);
                }

                AddNotification(Models.NotifyType.Success, "Records Successfully Updated.", true);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            ErrorNotification("Something failed.");
            return View(editUser);
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = _db.AspNetUsers.Where(u => u.Id == id).SingleOrDefault();
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.IsDelete = true;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
