using FSDProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSDProject.Controllers
{
    [Authorize]
    public class CascadeController : Controller
    {
        private ApplicationContext _Context;
        public CascadeController(ApplicationContext context)
        {
            _Context = context;
        }
        public JsonResult Country()
        {
            var cnt = _Context.Countries.ToList();
            return new JsonResult(cnt);
        }
        public JsonResult State(int id)
        {
            var st = _Context.States.Where(e => e.Country.Id == id).ToList();
            return new  JsonResult(st);
        }
        public JsonResult City(int id)
        {
            var ct = _Context.Cities.Where(e => e.State.Id == id).ToList();
            return new JsonResult(ct);
        }
        public IActionResult CascadeDropdown()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
