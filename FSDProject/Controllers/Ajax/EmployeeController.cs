using FSDProject.Data;
using FSDProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSDProject.Controllers.Ajax
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationContext _context;
        public EmployeeController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           return View();
        }
        public JsonResult EmployeeList()
        {
            var emp = _context.Employees.ToList();
            return new JsonResult(emp);
        }
        [HttpPost]
        public JsonResult AddEmployee(Employee employee)
        {
            var emp = new Employee()
            {
                Name = employee.Name,
                City = employee.City,
                State = employee.State,
                Salary = employee.Salary
            };

            _context.Employees.Add(emp);
            _context.SaveChanges();

            return new JsonResult("Data is saved");
        }

    }
}
