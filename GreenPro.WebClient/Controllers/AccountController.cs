using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using GreenPro.WebClient.Models;
using GreenPro.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;

namespace GreenPro.WebClient.Controllers
{

    public class AccountController : Controller
    {
        WorkflowMessageService _workflowMessageService;
        public AccountController()
        {
            _workflowMessageService = new WorkflowMessageService();
        }

        [Authorize]
        public ActionResult Profile()
        {

            GreenProDbEntities db = new GreenProDbEntities();

            var userid = User.Identity.GetUserId();
            AspNetUser user = db.AspNetUsers.Where(b => b.Id == userid).First();
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName", user.State);
            ViewBag.CityId = new SelectList(db.Cities.Where(b => b.StateID == user.State), "Id", "CityName", user.City);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);

        }
        [HttpPost]
        public ActionResult Profile(AspNetUser user, string CityId, string StateId)
        {
            try
            {
                GreenProDbEntities db = new GreenProDbEntities();

                ViewBag.StateId = new SelectList(db.States, "Id", "StateName", user.State);
                ViewBag.CityId = new SelectList(db.Cities.Where(b => b.StateID == user.State), "Id", "CityName", user.City);

                ModelState.Remove("State");
                ModelState.Remove("City");
                if (ModelState.IsValid)
                {
                    user.State = Convert.ToInt32(StateId);
                    user.City = Convert.ToInt32(CityId);

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "<p class='alert alert-info'>Saved Successfully</p>";
                    return View();
                }
                //ViewBag.Type = new SelectList(db.CarTypes, "Id", "Description", user.Type);

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "<p class='alert alert-danger'>Unable to Save: Technical Details:" + ex.Message + " </p>";

                return View();
            }
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        private void SetCookie(string cookieName, string cookieValue)
        {
            //Set Redirect URL Cookie for Transactional use in Paypal Tranasctions
            HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
            Response.Cookies.Remove(cookie.Name);
            cookie.Expires.AddMinutes(30);
            Response.SetCookie(cookie);
        }

        private HttpCookie GetCookie(string cookieName)
        {
            HttpCookie cookie;
            if (HttpContext.Request.Cookies[cookieName] != null)
            {
                cookie = HttpContext.Request.Cookies.Get(cookieName);
            }
            else
            {
                cookie = null;
            }
            return cookie;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            SetCookie("returnURL", returnUrl);

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }
        [AllowAnonymous]
        public ActionResult AdminRegister()
        {
            var user = UserManager.FindByName("kunal@innoator.com");
            if (user == null)
            {
                var adminUSer = new ApplicationUser()
                {
                    UserName = "kunal@innoator.com",
                    FirstName = "admin",
                    LastName = "LastName",
                    DateofBirth = DateTime.Now.Date,
                    Address = "address",
                    State = 1,
                    City = 1001,
                    Pincode = "21212",
                    Email = "kunal@innoator.com"
                };
                var result = UserManager.Create(adminUSer, "kunals");
            }

            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, string provider, string source = null)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }


            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var returnURL = GetCookie("returnURL");
                    if (returnURL != null)
                    {
                        return RedirectToLocal(returnURL.Value);
                    }

                    return RedirectToAction("Index", "Carusers");


                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View();
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            LoadStatesAndCity();


            return View();
        }

        private void LoadStatesAndCity()
        {
            GreenProDbEntities db = new GreenProDbEntities();
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string stateId)
        {
            LoadStatesAndCity();
            ModelState.Remove("Email");
            model.Email = model.Email;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { Email = model.UserName, UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName, DateofBirth = model.DateofBirth, Address = model.Address, State = model.StateId, City = model.CityId, Pincode = model.Pincode, EmailConfirmed = true };
                user.PhoneNumber = model.PhoneNumber;
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Add user to USERS Roles

                    await UserManager.AddToRoleAsync(user.Id, "Users");

                    //SEnd Mail Code
                    // Send an email with this link
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    SignInManager.PasswordSignIn(user.UserName, model.Password, isPersistent: false, shouldLockout: false);
                    //await UserManager.SendEmailAsync(user.Id, "Welcome- GreenPro", "Thanks For Registering with Green Pro");

                    // Customer Notification
                    _workflowMessageService.SendWelcomeMailtoCustomer(user.FirstName + " " + user.LastName, user.UserName, user.Email);

                    // New Customer Notification To Admin
                    _workflowMessageService.SendNewUserRegisterMailtoAdmin(user.FirstName + " " + user.LastName, user.UserName, user.Email, user.PhoneNumber);

                   
                    return RedirectToAction("Index", "CarUsers", null);
                }
                AddErrors(result);
            }


            return View(model);
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

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }
        [AllowAnonymous]
        public ActionResult PopupLogin()
        {
            return View();
        }
        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            GreenProDbEntities db = new GreenProDbEntities();
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            string firstname = string.Empty, lastname = string.Empty, emailid = string.Empty, userid = string.Empty, username = string.Empty;

            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var returnURL = GetCookie("returnURL");
                    if (returnURL != null)
                    {
                        RedirectToLocal(returnURL.Value);
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    //Get Provider
                    if (loginInfo.Login.LoginProvider == "Facebook")
                    {
                        firstname = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:first_name").Value;
                        lastname = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:last_name").Value;
                    }
                    else if (loginInfo.Login.LoginProvider == "Google")
                    {
                        var externalIdentity = AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                        var emailClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                        var lastNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
                        var givenNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);

                        emailid = emailClaim.Value;
                        firstname = givenNameClaim.Value;
                        lastname = lastNameClaim.Value;

                    }

                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.Email, FirstName = firstname, LastName = lastname });

            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            GreenProDbEntities db = new GreenProDbEntities();
            ModelState.Remove("Email");
            model.Email = model.UserName;

            ViewBag.StateId = new SelectList(db.States, "Id", "StateName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "CarUsers");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { Email = model.UserName, UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName, DateofBirth = model.DateofBirth, Address = model.Address, State = model.StateId, City = model.CityId, Pincode = model.Pincode, EmailConfirmed = true };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        //Add user to rol
                        await UserManager.AddToRoleAsync(user.Id, "Users");

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Welcome- GreenPro", "Thanks For Registering with Green Pro");

                        // ViewBag.Link = callbackUrl;
                        var returnURL = GetCookie("returnURL");
                        if (returnURL != null)
                        {
                            RedirectToLocal(returnURL.Value);
                        }
                        return RedirectToAction("Index", "CarUsers", null);

                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}