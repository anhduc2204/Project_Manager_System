using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BTL_LTWeb.Models
{
    public class ChatChannel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ChannelName { get; set; }
        public int ChannelType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ChannelMember> ChannelMembers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
