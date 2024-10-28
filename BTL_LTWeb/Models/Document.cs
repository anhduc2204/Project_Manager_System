using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_LTWeb.Models
{
    public class Document
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TaskId { get; set; }
        public Guid UploadBy { get; set; }

        [Required]
        public string DocumentName { get; set; }

        [Required]
        public string DocumentPath { get; set; }
        public long? FileSize { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }

        [ForeignKey("UploadBy")]
        public virtual User UploadByUser { get; set; }
    }
}
