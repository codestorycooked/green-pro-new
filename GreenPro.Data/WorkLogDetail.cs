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
    
    public partial class WorkLogDetail
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int SideId { get; set; }
        public string PrePost { get; set; }
        public string ImageRef { get; set; }
        public int WorkDoneId { get; set; }
    
        public virtual LogDetailCarSide LogDetailCarSide { get; set; }
        public virtual WorkDone WorkDone { get; set; }
    }
}
