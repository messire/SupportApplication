using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportApplication.Core.Context;
using SupportApplication.Core.Model;
using SupportApplication.Core.Repository;
using SupportApplication.Tests.Extensions;

namespace SupportApplication.Tests
{
    [TestClass]
    public class CrudTest
    {
        private Repository<Ticket> _repo;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = FixtureExtesion.CreateFixture();
           _repo = new Repository<Ticket>(new SupportEntities());
            
            
        }

        [TestMethod]
        public void CreateTicket()
        {
            var ticket = _fixture.Build<Ticket>().With(t => t.Status, TicketStatus.Open).Create();

            _repo.Create(ticket);
        }

        [TestMethod]
        public void GetAllTickets()
        {
            var list = _repo.GetAll().ToList();
            list.ForEach(l=>Debug.WriteLine(l.Name));
        }
    }
}
