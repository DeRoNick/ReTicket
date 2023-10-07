using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReTicket.MVC.Controllers
{
    public class TicketListingController : Controller
    {
        // GET: TicketListingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TicketListingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketListingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketListingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketListingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TicketListingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketListingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TicketListingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
