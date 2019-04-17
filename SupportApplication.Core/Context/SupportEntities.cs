using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportApplication.Core.Model;

namespace SupportApplication.Core.Context
{
    public class SupportEntities : DbContext
    {
        public SupportEntities() : base("SupportConnection")
        {
            Database.SetInitializer<SupportEntities>(new Initializer());
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Configurations.Add(new TicketConfiguration());
            modelBuilder.Configurations.Add(new TicketHistoryConfiguration());
        }
    }
}
