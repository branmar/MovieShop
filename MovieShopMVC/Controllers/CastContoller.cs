using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class CastContoller : Controller
    {
        // GET: CastContoller
        public ActionResult Index()
        {
            return View();
        }

        // GET: CastContoller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CastContoller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CastContoller/Create
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

        // GET: CastContoller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CastContoller/Edit/5
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

        // GET: CastContoller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CastContoller/Delete/5
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
