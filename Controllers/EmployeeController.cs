using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View(DapperOrm.ReturnList<EmployeeModel>("EmployeeViewAll"));
        }
        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            if (id is 0) return View();
            var param = new DynamicParameters();
                param.Add("@employeeId",id);
                return View(DapperOrm.ReturnList<EmployeeModel>("EmployeeViewById", param)
                    .FirstOrDefault<EmployeeModel>());

        }

        [HttpPost]
        public ActionResult AddorEdit(EmployeeModel emp)
        {
            var param = new DynamicParameters();
            param.Add("@employeeId",emp.EmployeeId);
            param.Add("@name",emp.Name);
            param.Add("@position",emp.Position);
            param.Add("@age",emp.Age);
            param.Add("@salary",emp.Salary);
           DapperOrm.ExceptionWithoutReturn("EmployeeAddOrEdit", param);
           return RedirectToAction("Index");
        }
        public ActionResult Delete (int id)
        {
            var param = new DynamicParameters();
            param.Add("@employeeId", id);
            DapperOrm.ExceptionWithoutReturn("employeeDeleteBYid", param);
            return RedirectToAction("Index");
        }
    }
}