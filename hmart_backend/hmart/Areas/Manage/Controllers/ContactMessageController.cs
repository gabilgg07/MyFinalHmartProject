using hmart.DAL;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ContactMessageController : Controller
    {
        private readonly AppDbContext _context;

        public ContactMessageController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ContactMessage> contactMessages = _context.ContactMessages.ToList();

            return View(contactMessages);
        }

        public IActionResult Delete(int id)
        {
            ContactMessage contactMessage = _context.ContactMessages.FirstOrDefault(x => x.Id == id);

            if (contactMessage == null) return PartialView("_NotFoundPartial");

            return View(contactMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ContactMessage cM)
        {
            ContactMessage contactMessage = _context.ContactMessages.FirstOrDefault(x => x.Id == cM.Id);

            if (contactMessage == null) return PartialView("_NotFoundPartial");

            _context.ContactMessages.Remove(contactMessage);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
