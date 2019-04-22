using System;
using System.Collections.Generic;

namespace SupportApplication.Core.Model
{
    public class Ticket
    {
        private TicketStatus _status;

        public string Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public TicketStatus Status
        {
            get => _status;
            set
            {
                if (value == _status) return;

                TicketHistoryCollection.Add(new TicketHistory(this, _status, value));
                _status = value;
            }
        }

        public virtual ICollection<TicketHistory> TicketHistoryCollection { get; set; }

        public Ticket()
        {
            Guid = System.Guid.NewGuid().ToString();
            TicketHistoryCollection = new List<TicketHistory> {new TicketHistory(this)};
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Ticket ticket)) return false;
            
            return Name == ticket.Name && Status == ticket.Status;
        }

        protected bool Equals(Ticket other) => _status == other._status &&
                                               string.Equals(Guid, other.Guid) &&
                                               string.Equals(Name, other.Name) &&
                                               string.Equals(Description, other.Description);
    }
}
