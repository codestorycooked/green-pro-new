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

        [Route("allpackages")]
        [HttpGet]
        public PackagesResponse Index()
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
            return model;
        }
    }
}
