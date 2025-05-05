using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCPractice.Models;

namespace MVCPractice.Controllers
{
    public class EmployeeController : Controller
    {
       
        private readonly dbContext db;

        public EmployeeController(dbContext _db)
        {
            db = _db;
        }
        public ActionResult Index()
        {
            try
            {
                ModelState.Clear();
                var Emlist = db.GetAllEmployee();
                if (Emlist.Count == 0)
                {
                    TempData["Error"] = "No data Found";
                }

                return View(Emlist);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }

        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
       
        public ActionResult Create(EmployeeModel obj)
        {
            try
            {
               bool isinserted = false;
                if (ModelState.IsValid) {

                    isinserted = db.Create(obj);
                    if (isinserted)
                    {
                        TempData["Success"] = "Data Saved Successfully";
                    }
                    else
                    {
                        TempData["Error"] = "Something went wrong";
                    }
                }
                return View(obj);

            }

            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
               return  View(obj);
            }
           
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
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

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
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
