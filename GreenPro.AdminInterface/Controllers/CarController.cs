using GreenPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenPro.AdminInterface.ViewModels;

namespace GreenPro.AdminInterface.Controllers
{
    public class CarController : BaseController
    {

        private GreenProDbEntities db;

        public CarController()
        {
            db = new GreenProDbEntities();
        }

        // GET: Car
        public ActionResult Index()
        {
            var carUsers = db.CarUsers.ToList();

            CarListViewModel model = new CarListViewModel();
            if (carUsers.Count > 0)
            {
                CarViewModel carModel;
                foreach (var item in carUsers)
                {
                    carModel = new CarViewModel();
                    carModel.AutoRenewal = item.AutoRenewal;
                    carModel.CarId = item.CarId;
                    carModel.Color = item.Color;
                    carModel.DisplayName = item.DisplayName;
                    carModel.GarageId = item.GarageId;
                    carModel.IsDeleted = item.IsDeleted;
                    carModel.LicenseNumber = item.LicenseNumber;
                    carModel.Make = item.Make;
                    carModel.PurchaseYear = item.PurchaseYear;
                    //carModel.Type = item.Type; //comment by circus
                    carModel.UserId = item.UserId;
                    carModel.CustomerName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName + "<br>" + item.AspNetUser.Email;
                    //carModel.CarType = item.CarType.Description; //comment by circus
                    if (item.Garage != null)
                        carModel.Garage = item.Garage.Garage_Name;

                    model.Cars.Add(carModel);
                }

            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            CarViewModel carModel = new CarViewModel();

            var item = db.CarUsers.Where(c => c.CarId == id).SingleOrDefault();

            if (item == null)
                return RedirectToAction("Index");

            carModel = new CarViewModel();
            carModel.AutoRenewal = item.AutoRenewal;
            carModel.CarId = item.CarId;
            carModel.Color = item.Color;
            carModel.DisplayName = item.DisplayName;
            carModel.GarageId = item.GarageId;
            carModel.IsDeleted = item.IsDeleted;
            carModel.LicenseNumber = item.LicenseNumber;
            carModel.Make = item.Make;
            carModel.PurchaseYear = item.PurchaseYear;
            //carModel.Type = item.Type; //comment by circus
            carModel.UserId = item.UserId;

            // carModel.CarType = item.CarType.Description; //comment by circus
            if (item.Garage != null)
                carModel.Garage = item.Garage.Garage_Name;

            if (item.UserPackages.Count > 0)
            {
                var UserPackagesList = item.UserPackages.Where(i => i.PaymentRecieved == true).ToList();
                if (UserPackagesList.Count > 0)
                {
                    carModel.UserPackages = new List<UserPackageViewModel>();
                    foreach (var userPackage in UserPackagesList)
                    {
                        UserPackageViewModel userPackageModel = new UserPackageViewModel();
                        userPackageModel.Id = userPackage.Id;
                        userPackageModel.ActualPrice = userPackage.ActualPrice;
                        userPackageModel.TotalPrice = userPackage.TotalPrice;
                        userPackageModel.TipAmount = userPackage.TipAmount;
                        userPackageModel.ServiceDay = userPackage.ServiceDay;
                        userPackageModel.SubscribedDate = userPackage.SubscribedDate;

                        userPackageModel.Package = new PackageViewModel();
                        userPackageModel.Package.Package_Name = userPackage.Package.Package_Name;
                        userPackageModel.Package.Package_Description = userPackage.Package.Package_Description;
                        

                        userPackageModel.Services = new List<CarServiceViewModel>();

                        var ServiceList = userPackage.Package.Package_Services.Select(s => s.Service).ToList();
                        foreach (var service in ServiceList)
                        {
                            CarServiceViewModel serviceModel = new CarServiceViewModel();
                            serviceModel.Service_Name = service.Service_Name;
                            userPackageModel.Services.Add(serviceModel);
                        }

                        if (userPackage.UserPackagesAddons.Count > 0)
                        {
                            foreach (var userPackagesAddon in userPackage.UserPackagesAddons)
                            {
                                CarServiceViewModel serviceModel = new CarServiceViewModel();
                                serviceModel.Service_Name = userPackagesAddon.Service.Service_Name;
                                serviceModel.IsAddOn = true;
                                userPackageModel.Services.Add(serviceModel);
                            }
                        }

                        carModel.UserPackages.Add(userPackageModel);

                    }
                }
            }



            return View(carModel);
        }
    }
}