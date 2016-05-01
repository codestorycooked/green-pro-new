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
    
    public partial class WorkDone
    {
        public WorkDone()
        {
            this.WorkLogDetails = new HashSet<WorkLogDetail>();
        }
    
        public int Id { get; set; }
        public string CrewLeaderID { get; set; }
        public string CrewMemberId { get; set; }
        public int PackageId { get; set; }
        public Nullable<System.DateTime> StartTimeStamp { get; set; }
        public Nullable<System.DateTime> EndTimeStamp { get; set; }
        public bool IsCompleted { get; set; }
        public Nullable<System.DateTime> CreatedTimeStamp { get; set; }
        public string UserId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual Package Package { get; set; }
        public virtual ICollection<WorkLogDetail> WorkLogDetails { get; set; }
    }
}
