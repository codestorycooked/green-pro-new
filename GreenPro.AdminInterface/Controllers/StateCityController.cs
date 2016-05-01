using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using GreenPro.Data;

namespace GreenPro.AdminInterface.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StateCityController : BaseController
    {
        private GreenProDbEntities db = new GreenProDbEntities();

        // GET: /StateCity/
        public async Task<ActionResult> Index()
        {
            var cities = db.Cities.Include(c => c.State);
            return View(await cities.ToListAsync());
        }

        public async Task<ActionResult> ShowCity(int id)
        {
            var cities = db.Cities.Where(a => a.StateID == id);
            return View(await cities.ToListAsync());
        }

       

        // GET: /StateCity/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: /StateCity/Create
        public ActionResult Create()
        {
            ViewBag.StateID = new SelectList(db.States, "Id", "StateName");
            return View();
        }

        // POST: /StateCity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,CityName,StateID")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StateID = new SelectList(db.States, "Id", "StateName", city.StateID);
            return View(city);
        }

        // GET: /StateCity/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateID = new SelectList(db.States, "Id", "StateName", city.StateID);
            return View(city);
        }

        // POST: /StateCity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,CityName,StateID")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.States, "Id", "StateName", city.StateID);
            return View(city);
        }

        // GET: /StateCity/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: /StateCity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            City city = await db.Cities.FindAsync(id);
            db.Cities.Remove(city);
            await db.SaveChangesAsync();
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
