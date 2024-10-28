using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_LTWeb.Models
{
    public class OrganizationUser
    {
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdminOrganization { get; set; } = false;

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
