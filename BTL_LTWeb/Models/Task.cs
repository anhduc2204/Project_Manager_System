using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_LTWeb.Models
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProjectId { get; set; }

        [Required]
        [MaxLength(255)]
        public string TaskName { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int Status { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public virtual ICollection<UserTask> UserTasks { get; set; }
        public virtual ICollection<TaskComment> TaskComments { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
