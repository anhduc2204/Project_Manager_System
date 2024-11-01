using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_LTWeb.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? OrganizationId { get; set; } = null;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Status { get; set; }

        [ForeignKey("OrganizationId")] public virtual Organization? Organization { get; set; } = null;

        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }

        public virtual ICollection<UserProject> UserProjects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
