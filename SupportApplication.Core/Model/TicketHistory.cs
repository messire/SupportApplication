using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportApplication.Core.Model
{
    public class TicketHistory
    {
        public string Guid { get; set; }
        public virtual Ticket Ticket { get; set; }
        public TicketStatus ChangedFrom { get; set; }
        public TicketStatus ChangedTo { get; set; }
        public DateTime DateTime { get; set; }

        public TicketHistory() => Guid = System.Guid.NewGuid().ToString();
    }
}
