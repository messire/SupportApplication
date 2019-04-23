using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupportApplication.Core.Context;
using SupportApplication.Core.Model;
using SupportApplication.Core.Repository;

namespace SupportApplication.UI.Controllers
{
    public class ManageController : Controller
    {
        private readonly Repository<Ticket> _repo = new Repository<Ticket>();
        private readonly Repository<TicketHistory> _history = new Repository<TicketHistory>();

        public ActionResult Index()
        {
            return View(_repo.Get(t => t.Status != TicketStatus.Deleted));
        }

        public ActionResult Create()
        {
            IEnumerable<SelectListItem> selectList = Enum.GetValues(typeof(TicketStatus))
                .Cast<TicketStatus>()
                .Where(t => t == TicketStatus.Open)
                .Select(t => new SelectListItem
                {
                    Value = ((int)t).ToString(),
                    Text = t.ToString()
                });

            ViewBag.selectList = selectList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket)
        {
            if (!ModelState.IsValid) return View(ticket);

            _repo.Create(ticket);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Ticket ticket = _repo.FindById(id);

            if (ticket == null) return HttpNotFound();

            IEnumerable<SelectListItem> selectList = new List<SelectListItem>();

            if (ticket.Status == TicketStatus.Open)
                selectList = Enum.GetValues(typeof(TicketStatus))
                    .Cast<TicketStatus>()
                    .Where(t => t == TicketStatus.Open || t == TicketStatus.Resolved)
                    .Select(t => new SelectListItem
                    {
                        Value = ((int) t).ToString(),
                        Text = t.ToString()
                    });
            else if (ticket.Status == TicketStatus.Resolved)
                selectList = Enum.GetValues(typeof(TicketStatus))
                    .Cast<TicketStatus>()
                    .Where(t => t == TicketStatus.Resolved || t == TicketStatus.Rejected || t == TicketStatus.Closed)
                    .Select(t => new SelectListItem
                    {
                        Value = ((int) t).ToString(),
                        Text = t.ToString()
                    });
            else if (ticket.Status == TicketStatus.Rejected)
                selectList = Enum.GetValues(typeof(TicketStatus))
                    .Cast<TicketStatus>()
                    .Where(t => t == TicketStatus.Rejected || t == TicketStatus.Resolved)
                    .Select(t => new SelectListItem
                    {
                        Value = ((int) t).ToString(),
                        Text = t.ToString()
                    });

            var a = ModelState;

            ViewBag.selectList = selectList;

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket ticket, string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                ViewBag.CommentError = "Comment field is required!";
                ModelState.AddModelError("Comment", ViewBag.CommentError);
            }

            if (!ModelState.IsValid) return RedirectToAction("Edit", "Manage", new {ticket.Guid});

            Ticket updating = _repo.FindById(ticket.Guid);
            updating.TicketHistoryCollection = new List<TicketHistory>();
            updating.Description = ticket.Description;
            updating.Status = ticket.Status;
            updating.TicketHistoryCollection.Last().Comment = comment;

            _repo.Update(updating);
            ViewBag.CommentError = string.Empty;

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = _repo.FindById(id);

            return ticket == null ? (ActionResult) HttpNotFound() : View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ticket ticket = _repo.FindById(id);

            ticket.Status = TicketStatus.Deleted;

            _repo.Update(ticket);
            
            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            return View(_history.GetAll());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
