using GreenPro.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GreenPro.Api.Controllers
{
    [RoutePrefix("api/packages")]
    public class PackagesController : ApiController
    {

        GreenPro.Data.GreenProDbEntities _db;
        public PackagesController()
        {
            _db = new Data.GreenProDbEntities();

        }

        [Route("AllPackages")]
        [HttpGet]
        public dynamic GetPackages()
        {
            // Store Customer Seleted Garage in session
            //Session["NewServiceGarageId"] = id;

            PackagesResponse model = new PackagesResponse();

            try
            {
                var packages = _db.Packages.ToList();
                PackageViewModel package = null;
                foreach (var item in packages)
                {
                    package = new PackageViewModel();
                    package.PackageId = item.PackageId;
                    package.Package_Description = item.Package_Description;
                    package.Package_Name = item.Package_Name;
                    package.Package_Price = item.Package_Price;
                    package.SubscriptionTypes = item.SubscriptionTypes;

                    foreach (var service in item.Package_Services.ToList())
                    {
                        package.Services.Add(service.Service.Service_Name);
                    }

                    model.Packages.Add(package);
                }
                model.Result = true;
            }
            catch (Exception ex)
            {

            }

            //return Ok(model);
            return model.Packages;
        }

        [Route("AllAddOns")]
        [HttpGet]
        public dynamic GetAddons()
        {
            ServiceAddOnsResponse model = new ServiceAddOnsResponse();
            try
            {

                var service_Add = _db.Services.Where(a => a.IsAddOn == true).ToList();
                Data.Service serviceAddons = null;
                foreach (var item in service_Add)
                {
                    serviceAddons = new Data.Service();
                    serviceAddons.Service_Description = item.Service_Description;
                    serviceAddons.Service_Name = item.Service_Name;
                    serviceAddons.Service_Price = item.Service_Price;
                    serviceAddons.ServiceID = item.ServiceID;
                    model.data.Add(serviceAddons);
                }
                model.Result = true;
                return model.data;
            }
            catch (Exception ex)
            {
                model.Result = false;
                model.ResponseMessage = ex.Message;
                return model;

            }


        }
    }
}
