using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using GreenPro.Data;
using LumenWorks.Framework.IO.Csv;

namespace GreenPro.AdminInterface.Controllers
{
    public class CommonController : BaseController
    {
        //
        // GET: /Common/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStatesAndCityData()
        {
            


            string statesPath = Server.MapPath(@"~\Data\states.csv");
            //@"C:\Users\priyam\Documents\Visual Studio 2013\Projects\AddStateCityInDB\states-1.csv";
            var stateInfo = new CachedCsvReader(new StreamReader(statesPath), true);

            string citiesPath = Server.MapPath(@"~\Data\cities.csv");
            //@"C:\Users\priyam\Documents\Visual Studio 2013\Projects\AddStateCityInDB\cities-1.csv";
            var cityInfo = new CachedCsvReader(new StreamReader(citiesPath), true);

            var statesData = stateInfo.ToList();
            var citiesData = cityInfo.ToList();

            var db = new GreenProDbEntities();

           // db.Cities.RemoveRange(db.Cities);
          //  db.SaveChanges();

           // db.States.RemoveRange(db.States);
           // db.SaveChanges();
            
            
            foreach (var state in statesData)
            {
                var stateToBeSaved = new State { StateName = state[1] };
                db.States.Add(stateToBeSaved);
                db.SaveChanges();
                var cityList = citiesData.Where(a => a[0] == state[0]).Select(a => a[1]).ToList();
                var cityNames = new List<string>();
                foreach (var city in cityList)
                {
                    if (!cityNames.Contains(city))
                    {
                        cityNames.Add(city);
                        db.Cities.Add(new City { CityName = city, StateID = stateToBeSaved.Id });
                    }
                }
                cityNames.Clear();
                db.SaveChanges();
            }
            //db.SaveChanges();

            return View();
        }
    }
}