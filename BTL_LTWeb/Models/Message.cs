using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_LTWeb.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ChannelId { get; set; }
        public Guid UserId { get; set; }

        [Required]
        public string MessageContent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ChannelId")]
        public virtual ChatChannel ChatChannel { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
