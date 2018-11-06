using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamelistBuilder.Infrastructure;
using GamelistBuilder.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamelistBuilder.Controllers
{
    public class PlatformController : Controller
    {
        private readonly IRepository<Platform> _platform;

        public PlatformController(IRepository<Platform> platform)
        {
            _platform = platform;
        }
        public IActionResult Index()
        {
            var platforms = _platform.GetAll();

            return View(platforms);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _platform.GetById(id);
         
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Platform data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            };
            _platform.Update(data);
            return RedirectToAction("Index");
        }

    }
}