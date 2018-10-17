using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamelistBuilder.Infrastructure;
using GamelistBuilder.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamelistBuilder.Controllers
{
    public class GamelistController : Controller
    {
        private IRepository<Gamelist> _repository;

        public GamelistController(IRepository<Gamelist> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _repository.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var data = new Gamelist();
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(Gamelist data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            };
            _repository.Create(data);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var data = _repository.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Gamelist data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            };
            _repository.Update(data);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var data = _repository.GetById(id);
            _repository.Delete(data);
            return RedirectToAction("Index");
        }

        public IActionResult Open (string id)
        {
            var gamelist = _repository.GetById(id);
            if (!System.IO.File.Exists(gamelist.Path))
            {
                ViewData["Error"] = "gamelist.xml not found at " + gamelist.Path;
                var list = _repository.GetAll();
                return View("Index", list);
            }
            var games = new GameRepository(gamelist.Path);
            gamelist.Games = games.GetAll().ToList();

            return View(gamelist);

        }
    }
}