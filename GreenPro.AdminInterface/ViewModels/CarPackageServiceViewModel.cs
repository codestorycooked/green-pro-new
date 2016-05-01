using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenPro.AdminInterface.ViewModels
{
    public class CarPackageServiceViewModel
    {
        public int WorkDoneId { get; set; }
        public WorkDone WorkDoneDetail { get; set; }
        public int CarId { get; set; }
        public int CarLicenseNumber { get; set; }
        public CarUser CarDetails { get; set; }
        public ServiceCarDetails Details { get; set; }
        public AspNetUser AssignedWorker { get; set; }
        public List<AspNetUser> UserList { get; set; }
        public SelectList UserSelectList { get; set; }
        public string AssignWorkerId { get; set; }
        //public List<CarUser> CarList { get; set; }
        //public List<AspNetUser> UserList { get; set; }
        //public int SelectedGarageId { get; set; }
        //public AspNetUser SelectedUser { get; set; }
        //public CarUser SelectedCar { get; set; }
        //public List<Package> SelectedPackage { get; set; }
        //public List<Service> SelectedPackageService { get; set; }
        //public List<WorkLogDetail> WorkLogDetailList { get; set; }
        //public WorkDone Work { get; set; }
        //public List<WorkLogDetail> WorkLogs { get; set; }
    }

    public class ServiceCarDetails
    {
        public CarUser CarDetails { get; set; }
        public Package PackageDetails { get; set; }
        public List<GreenPro.Data.Service> ServiceDetails { get; set; }
        public List<WorkLogDetail> PreWorkLogs { get; set; }
        public List<WorkLogDetail> PostWorkLogs { get; set; }
        public WorkLogDetail NewWorkLogs { get; set; }
    }
}