﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
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

        public string Comment { get; set; }

        public TicketHistory()
        {
            Guid = System.Guid.NewGuid().ToString();
            ChangedFrom = TicketStatus.Created;
            DateTime = DateTime.Now;
            Comment = "Ticket created.";
        }

        public TicketHistory(Ticket ticket) : this()
        {
            Ticket = ticket;
        }

        public TicketHistory(Ticket ticket, TicketStatus oldStatus, TicketStatus newStatus) : this(ticket)
        {
            ChangedTo = newStatus;
            ChangedFrom = oldStatus;
        }

        public TicketHistory(Ticket ticket, TicketStatus oldStatus, TicketStatus newStatus, string comment)
            : this(ticket, oldStatus, newStatus) => Comment = comment;
    }
}
