//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreenPro.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Garage
    {
        public Garage()
        {
            this.CarUsers = new HashSet<CarUser>();
            this.GarageMaxCars = new HashSet<GarageMaxCar>();
            this.GarrageWeekdays = new HashSet<GarrageWeekday>();
            this.LeaderCarJobs = new HashSet<LeaderCarJob>();
            this.UnAssignedCars = new HashSet<UnAssignedCar>();
            this.WorkerGarages = new HashSet<WorkerGarage>();
        }
    
        public int GarageId { get; set; }
        public string Garage_Name { get; set; }
        public string Garage_Address { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public double Latitute { get; set; }
        public double Longitude { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string CreatedBy { get; set; }
        public string Contact_Person { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public Nullable<System.TimeSpan> OpenTime { get; set; }
        public System.TimeSpan CloseTime { get; set; }
        public string ServiceDays { get; set; }
    
        public virtual ICollection<CarUser> CarUsers { get; set; }
        public virtual City City1 { get; set; }
        public virtual ICollection<GarageMaxCar> GarageMaxCars { get; set; }
        public virtual State State1 { get; set; }
        public virtual ICollection<GarrageWeekday> GarrageWeekdays { get; set; }
        public virtual ICollection<LeaderCarJob> LeaderCarJobs { get; set; }
        public virtual ICollection<UnAssignedCar> UnAssignedCars { get; set; }
        public virtual ICollection<WorkerGarage> WorkerGarages { get; set; }
    }
}
