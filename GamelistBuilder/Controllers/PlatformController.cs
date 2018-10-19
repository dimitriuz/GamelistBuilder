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
    }
}