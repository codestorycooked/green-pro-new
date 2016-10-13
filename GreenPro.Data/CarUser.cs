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
    
    public partial class CarUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarUser()
        {
            this.AdhocUserPackages = new HashSet<AdhocUserPackage>();
            this.CarServiceEntries = new HashSet<CarServiceEntry>();
            this.LeaderCarJobs = new HashSet<LeaderCarJob>();
            this.UserPackages = new HashSet<UserPackage>();
        }
    
        public int CarId { get; set; }
        public string DisplayName { get; set; }
        public string Make { get; set; }
        public string LicenseNumber { get; set; }
        public string Color { get; set; }
        public int PurchaseYear { get; set; }
        public string UserId { get; set; }
        public bool AutoRenewal { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> GarageId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdhocUserPackage> AdhocUserPackages { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarServiceEntry> CarServiceEntries { get; set; }
        public virtual Garage Garage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaderCarJob> LeaderCarJobs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPackage> UserPackages { get; set; }
    }
}
