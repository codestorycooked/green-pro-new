using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceLocator src = new ServiceLocator();
            AutomateWorkDistribution at = new AutomateWorkDistribution();
            //at.DistributeCars();
            //src.AssignJob();
            BillGenerator obj = new BillGenerator();
            obj.AutoPay();
        }
    }
}
