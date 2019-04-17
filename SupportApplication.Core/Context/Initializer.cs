using System.Collections.Generic;
using System.Data.Entity;
using SupportApplication.Core.Model;

namespace SupportApplication.Core.Context
{
    public class Initializer : //DropCreateDatabaseAlways<HoroscopeEntities>
        CreateDatabaseIfNotExists<SupportEntities>
    {
        protected override void Seed(SupportEntities ctx)
        {
            //CreateCitiesContent(ctx);
        }

        private void CreateCitiesContent(SupportEntities ctx)
        {
            //List<Ticket> entries = GenerateListSigns();

            //ctx.Tickets.AddRange(entries);
            //ctx.SaveChanges();
        }

        private List<Ticket> GenerateListSigns()
        {

            return new List<Ticket>();
        }
    }
}
