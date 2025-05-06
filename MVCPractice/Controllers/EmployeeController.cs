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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var EmpUpdate = db.GetAllEmployee().Find(obj => obj.Id == id);
            if(EmpUpdate == null)
            {
                TempData["Success"] = "No data found";
                return RedirectToAction("Index");
            }
            return View(EmpUpdate);
        }

       
        [HttpPost]
        public ActionResult Edit(EmployeeModel obj)
        {
            try
            {
                bool isUpdated = false;
                if (ModelState.IsValid)
                {

                    isUpdated = db.Update(obj);
                    if (isUpdated)
                    {
                        TempData["Success"] = "Data Update Successfully";
                    }
                    else
                    {
                        TempData["Error"] = "Something went wrong";
                    }
                }
                return RedirectToAction("Index");

            }

            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        
        public ActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = false;
                if (ModelState.IsValid)
                {

                    isDeleted = db.Delete(id);
                    if (isDeleted)
                    {
                        TempData["Success"] = "Data Deleted Successfully";
                       
                    }
                    else
                    {
                        TempData["Error"] = "Something went wrong";
                       
                    }
                }
                return RedirectToAction("Index");

            }

            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
