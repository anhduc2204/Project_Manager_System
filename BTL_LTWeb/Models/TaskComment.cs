using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_LTWeb.Models
{
    public class TaskComment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }

        [Required]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
