using System;
using System.Collections.Generic;

namespace SupportApplication.Core.Model
{
    public class Ticket
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }

        public virtual ICollection<TicketHistory> TicketHistory { get; set; }

        public Ticket()
        {
            Guid = System.Guid.NewGuid().ToString();
            TicketHistory = new List<TicketHistory>
            {
                new TicketHistory
                {
                    Ticket = this,
                    DateTime = DateTime.Now,
                    ChangedFrom = TicketStatus.Created,
                    ChangedTo = TicketStatus.Open
                }
            };
        }
    }
}
