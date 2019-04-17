using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SupportApplication.Core.Model;

namespace SupportApplication.Core.Context
{
    public class TicketConfiguration : EntityTypeConfiguration<Ticket>
    {
        private readonly string _indexName = "ix_ticket_guid";

        public TicketConfiguration()
        {
            HasKey(t => t.Guid);
            //Property(t => t.Guid).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(_indexName)));
            Property(t => t.Description).HasMaxLength(4000);
        }
    }

    public class TicketHistoryConfiguration : EntityTypeConfiguration<TicketHistory>
    {
        private readonly string _indexName = "ix_ticket_history_datetime";

        public TicketHistoryConfiguration()
        {
            HasKey(t => t.Guid);
            Property(th => th.DateTime).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(_indexName)));
        }
    }
}
