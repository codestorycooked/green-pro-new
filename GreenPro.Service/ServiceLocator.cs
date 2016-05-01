using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenPro.Data;
namespace GreenPro.Service
{
    public class ServiceLocator
    {
        private readonly string _day;
        private readonly GreenProDbEntities _dbEntities;

        public ServiceLocator()
        {
            _dbEntities = new GreenProDbEntities();
            _day = DateTime.Now.DayOfWeek.ToString();
        }

        public void AssignJob()
        {
            var currentDay = DateTime.Now.Date.DayOfWeek.ToString();
            var garageList = _dbEntities.Garages.Where(a => a.ServiceDays == _day).ToList();


            List<int> carCount=null;
            foreach (var garage in garageList)
            {
                carCount = (
                    from p in _dbEntities.Packages
                    from u in _dbEntities.UserPackages
                    from c in _dbEntities.CarUsers
                    where u.PackageId == p.PackageId && u.CarId == c.CarId && c.GarageId == garage.GarageId
                    select u.CarId).ToList();
            }
            //get all leaders
            var leaders = _dbEntities.WorkerGarages.Where(a => a.IsLeader);
            //get all garages for 
            var todayGarages = _dbEntities.Garages.Where(a => a.ServiceDays == _day);
            var counter = 01;
            var count = 0;
            //Truncate Table before use
            for (int i = 0; i < carCount.Count(); i++)
            {
                counter++;
                if (counter==leaders.Count())
                {
                    counter++;

                }
            }

         
        }


        private void GetNextLeader()
        {

        }

    }
}
