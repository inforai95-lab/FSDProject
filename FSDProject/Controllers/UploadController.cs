using FSDProject.Data;
using FSDProject.Models;
using FSDProject.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FSDProject.Controllers
{

    [Authorize]
    public class UploadController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _environment;
        public UploadController(ApplicationContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadImage(ImageCreateModel model)
        {
            if(ModelState.IsValid)
            {
                var path = _environment.WebRootPath;
                var filepath = "Content/Image/" + model.ImagePath.FileName;
                var fullpath = Path.Combine(path, filepath);
                UploadFile(model.ImagePath, fullpath);
                var dt = new Image()
                {
                    Name = model.Name,
                    ImagePath = filepath
                };
                _context.Add(dt);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
           
        }
        public void UploadFile( IFormFile file,string path)
        {
            FileStream Stream = new FileStream(path, FileMode.Create);
            file.CopyTo(Stream);
        }
        public IActionResult Index()
        {
            var data = _context.Images.ToList();

            return View(data);
        }
    }
}
