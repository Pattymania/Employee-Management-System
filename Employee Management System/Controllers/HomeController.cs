using Employee_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee_Management_System.Factories;
namespace Employee_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeManagementSystemEntities db = new EmployeeManagementSystemEntities();
        private EmployeeDetailFactory _employeeDetailFactory = new EmployeeDetailFactory();

        [AllowAnonymous]
        public ActionResult Login()
        {
            EmployeeInfoModel infoModel = new EmployeeInfoModel();
            return View(infoModel);
        }

        [HttpPost]
        public ActionResult Login(EmployeeInfoModel infoModel)
        {
            if (ModelState.IsValid)
            {
                var login = db.EmployeeInfoes.Where(i => i.Email == infoModel.Email.Trim() && i.Password == infoModel.Password.Trim()).Any();
                if (login == true)
                   return RedirectToAction("Index");
                else
                    return View("InvalidLogin");
            }
            return View();
        }

        public ActionResult InvalidLogin()
        {
            return View();
        }
        public ActionResult Index()
        {
            var employeeInfos = db.EmployeeInfoes.ToList();
            if (employeeInfos != null || employeeInfos.Count() != 0)
            {
                List<EmployeeInfoModel> employeeModels = new List<EmployeeInfoModel>();

                foreach (var employee in employeeInfos)
                {
                    EmployeeInfoModel employeeModel = new EmployeeInfoModel();
                    employeeModel = _employeeDetailFactory.PrepareEmployeeModelFromEmployee(employee);
                    employeeModels.Add(employeeModel);
                }
                return View(employeeModels);
            }

            else
                return View("NoRecords");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID, FirstName,	LastName,	DOB,	Department,	PhoneNumber,Email, Password")] EmployeeInfoModel infoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeObj = _employeeDetailFactory.PrepareEmployeeFromEmployeeModel(infoModel);
                    
                    db.EmployeeInfoes.Add(employeeObj);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit( int ID = 0)
        {
            if(ID == 0)
            {
                return HttpNotFound();
            }
            else
            {
                var employeeDetail = db.EmployeeInfoes.Find(ID);
                if(employeeDetail != null)
                {
                    var employeeModel = _employeeDetailFactory.PrepareEmployeeModelFromEmployee(employeeDetail);

                    return View(employeeModel);
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "EmployeeID, FirstName,	LastName,	DOB,	Department,	PhoneNumber,Email, Password")] EmployeeInfoModel infoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeDetailFactory.UpdateEmployeeFromEmployeeModel(infoModel);
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID = 0) 
        {
            if (ID == 0)
            {
                return HttpNotFound();
            }
            else
            {
                var employeeDetail = db.EmployeeInfoes.Find(ID);
                if (employeeDetail != null)
                {
                    var employeeModel = _employeeDetailFactory.PrepareEmployeeModelFromEmployee(employeeDetail);

                    return View(employeeModel);
                }
                return HttpNotFound();
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employees = db.EmployeeInfoes.Find(ID);
                    if (employees == null)
                        return HttpNotFound();
                    else
                    {
                        db.EmployeeInfoes.Remove(employees);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}