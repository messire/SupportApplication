﻿namespace SupportApplication.Core.Model
{
    public enum TicketStatus
    {
        Deleted = -2,
        Created = -1,
        Open = 0,
        Resolved = 1,
        Rejected = 2,
        Closed = 3
    }
}