using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GreenPro.Data;
using GreenPro.Api.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;

namespace GreenPro.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/carusers")]
    public class CarUsersController : ApiController
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public CarUsersController()
        {
        }

        public CarUsersController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


        [Route("usercarlist")]
        [HttpGet]
        public  IHttpActionResult UserCarList()
        {
            IdentityUser user =  UserManager.FindById(User.Identity.GetUserId());
            //var carUsers =  db.CarUsers.Where(u => u.AspNetUser.Id == "").ToList();      
            var carUsers = db.CarUsers.ToList();
            carUsers = db.CarUsers.Where(u => u.AspNetUser.Id == user.Id).ToList();
            UserCarListResponse response = new UserCarListResponse();
            CarUserViewModel carModel = null;
            foreach (var car in carUsers)
            {
                carModel = new CarUserViewModel();
                carModel.CarId = car.CarId;
                carModel.DisplayName = car.DisplayName;
                carModel.Color = car.Color;
                carModel.LicenseNumber = car.LicenseNumber;
                carModel.PurchaseYear = car.PurchaseYear;
                carModel.Make = car.Make;
                response.cars.Add(carModel);
            }
            return Ok(response);
        }

        // GET: api/CarUsers
        public IQueryable<CarUser> GetCarUsers()
        {
            return db.CarUsers;
        }

        // GET: api/CarUsers/5
        [ResponseType(typeof(CarUser))]
        public async Task<IHttpActionResult> GetCarUser(int id)
        {
            CarUser carUser = await db.CarUsers.FindAsync(id);
            if (carUser == null)
            {
                return NotFound();
            }

            return Ok(carUser);
        }

        // PUT: api/CarUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCarUser(int id, CarUser carUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carUser.CarId)
            {
                return BadRequest();
            }

            db.Entry(carUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CarUsers
        [ResponseType(typeof(CarUser))]
        public async Task<IHttpActionResult> PostCarUser(CarUser carUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarUsers.Add(carUser);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = carUser.CarId }, carUser);
        }

        // DELETE: api/CarUsers/5
        [ResponseType(typeof(CarUser))]
        public async Task<IHttpActionResult> DeleteCarUser(int id)
        {
            CarUser carUser = await db.CarUsers.FindAsync(id);
            if (carUser == null)
            {
                return NotFound();
            }

            db.CarUsers.Remove(carUser);
            await db.SaveChangesAsync();

            return Ok(carUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarUserExists(int id)
        {
            return db.CarUsers.Count(e => e.CarId == id) > 0;
        }
    }
}