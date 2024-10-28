using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTWeb.Models
{
    public class ChannelMember
    {
        public Guid ChannelId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ChannelId")]
        public virtual ChatChannel ChatChannel { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
