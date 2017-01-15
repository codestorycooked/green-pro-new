using GreenPro.Api.ViewModels.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GreenPro.Api.Controllers
{
    [RoutePrefix("api/garages")]
    public class GaragesController : ApiController
    {
        GreenPro.Data.GreenProDbEntities _db;
        public GaragesController()
        {
            _db = new Data.GreenProDbEntities();

        }

        [Route("allcitys")]
        [HttpGet]
        public IHttpActionResult Citys()
        {
            // Store Customer Seleted Garage in session
            //Session["NewServiceGarageId"] = id;



            CityResponseModel model = new CityResponseModel();

            try
            {
                var cityList = _db.Database.SqlQuery<CityModel>("exec dbo.GetAllAvailableGaragesCitiesList").ToList();
                CityModel cityModel = null;
                foreach (var item in cityList)
                {
                    cityModel = new CityModel();
                    cityModel.Id = item.Id;
                    cityModel.CityName = item.CityName;

                    model.Citys.Add(cityModel);
                }
                model.Result = true;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            if (model != null)
            {
                return Ok(model.Citys);
            }
            else
                return NotFound();
        }

        [Route("get-garagesby-city")]
        public IHttpActionResult GetGaragesByCity(int Id)
        {
            GaragesListRsponseModel model = new GaragesListRsponseModel();

            try
            {
                var garages = _db.Garages.AsQueryable();
                garages = garages.Where(b => b.IsActive == true);
                garages = garages.Where(b => b.City == Id);

                foreach (var item in garages.ToList())
                {
                    GarageModel gModel = new GarageModel();
                    gModel.GarageId = item.GarageId;
                    gModel.Garage_Name = item.Garage_Name;
                    gModel.Garage_Address = item.Garage_Address;
                    gModel.OpenTime = item.OpenTime;
                    gModel.Latitute = item.Latitute;
                    gModel.Longitude = item.Longitude;

                    if (item.OpenTime.HasValue)
                    {
                        DateTime opentime = DateTime.Today.Add(item.OpenTime.Value);
                        gModel.OpenTimeStr = opentime.ToString("hh:mm tt");
                    }
                    gModel.CloseTime = item.CloseTime;
                    DateTime closetime = DateTime.Today.Add(item.CloseTime);
                    gModel.CloseTimeStr = closetime.ToString("hh:mm tt");

                    gModel.ServiceDays = item.ServiceDays;
                    if (item.City1 != null)
                        gModel.City = item.City1.CityName;
                    if (item.State1 != null)
                        gModel.State = item.State1.StateName;
                    gModel.Pincode = item.Pincode;
                    model.Garages.Add(gModel);
                }
                model.Result = true;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            if (model != null)
            {
                return Ok(model.Garages);
            }
            else
                return NotFound();
        }

        [Route("getallgarages")]
        [HttpGet]
        public IHttpActionResult GetAllGarages()
        {

            GaragesListRsponseModel model = new GaragesListRsponseModel();

            try
            {
                var garages = _db.Garages.AsQueryable();
                garages = garages.Where(b => b.IsActive == true);


                foreach (var item in garages.ToList())
                {
                    GarageModel gModel = new GarageModel();
                    gModel.GarageId = item.GarageId;
                    gModel.Garage_Name = item.Garage_Name;
                    gModel.Garage_Address = item.Garage_Address;
                    gModel.OpenTime = item.OpenTime;
                    gModel.Latitute = item.Latitute;
                    gModel.Longitude = item.Longitude;

                    if (item.OpenTime.HasValue)
                    {
                        DateTime opentime = DateTime.Today.Add(item.OpenTime.Value);
                        gModel.OpenTimeStr = opentime.ToString("hh:mm tt");
                    }
                    gModel.CloseTime = item.CloseTime;
                    DateTime closetime = DateTime.Today.Add(item.CloseTime);
                    gModel.CloseTimeStr = closetime.ToString("hh:mm tt");

                    gModel.ServiceDays = item.ServiceDays;
                    if (item.City1 != null)
                        gModel.City = item.City1.CityName;
                    if (item.State1 != null)
                        gModel.State = item.State1.StateName;
                    gModel.Pincode = item.Pincode;
                    gModel.CityID = item.City1.Id;
                    gModel.StateID = item.State1.Id;
                    model.Garages.Add(gModel);
                }
                model.Result = true;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            if (model != null)
            {
                return Ok(model);
            }
            else
                return NotFound();

        }

        [Route("garageDetails")]
        [HttpGet]
        public IHttpActionResult GarageDetails(string id)
        {
            int garagdid = Convert.ToInt32(id);
            var model = from a in _db.Garages
                        where a.GarageId == garagdid
                        select a;
            if (model != null)
            {
                return Ok(model);
            }
            else
                return NotFound();

        }
    }
}
