using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GreenPro.Data
{
    [MetadataType(typeof(AspNetUserRolesMetadata))]
    public partial class AspNetUserRoles
    {
        class AspNetUserRolesMetadata
        {

            public String UserId { get; set; }

            public String RoleId { get; set; }
        }
    }
}
