using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamelistBuilder.Infrastructure;
using GamelistBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using GamelistBuilder.Helpers;
using GamelistBuilder.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamelistBuilder.Controllers
{
    public class GamelistController : Controller
    {
        private IRepository<Gamelist> _repository;
        private IRepository<Platform> _platformsRepository;
        private IRepository<Game> _gamesRepository;



        public GamelistController(IRepository<Gamelist> repository, IRepository<Platform> platforms, IRepository<Game> games)
        {
            _repository = repository;
            _platformsRepository = platforms;
            _gamesRepository = games;
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
            var data = new CreateGamelistViewModel();
            var platforms = _platformsRepository.GetAll();
            ViewBag.Platforms = new SelectList(platforms, "Id", "Name");
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(CreateGamelistViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            };

            var gamelist = new Gamelist
            {
                Description = data.Description,
                GamesDirectory = data.GamesDirectory,
                ImagesDirectory = data.ImagesDirectory,
                MarqueDirectory = data.MarqueDirectory,
                Name = data.Name,
                Path = data.Path,
                VideoDirectory = data.VideoDirectory,
                Games = new List<Game>(),
                GameFolders = new List<GameFolder>(),
                Platform =  _platformsRepository.GetById(data.PlatformId)
            };

            
            _repository.Create(gamelist);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var data = _repository.GetById(id);
            var dataVM = new CreateGamelistViewModel
            {
                Description = data.Description,
                GamesDirectory = data.GamesDirectory,
                VideoDirectory = data.VideoDirectory,
                PlatformId = data.Platform.Id,
                ImagesDirectory = data.ImagesDirectory,
                MarqueDirectory = data.MarqueDirectory,
                Name = data.Name,
                Path = data.Path
            };
            var platforms = _platformsRepository.GetAll();
            ViewBag.Platforms = new SelectList(platforms, "Id", "Name");
            return View(dataVM);
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

        [HttpPost]
        public IActionResult EditGame(Game data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            };
            _gamesRepository.Update(data);
            return RedirectToAction("Index");
        }

        public IActionResult EditGame(int id)
        {
            var data = _gamesRepository.GetById(id);
            return View(data);
        }


        public IActionResult Delete(int id)
        {
            var data = _repository.GetById(id);
            _repository.Delete(data);
            return RedirectToAction("Index");
        }

        public IActionResult Open (int id)
        {
            var gamelist = _repository.GetById(id);
            GamelistHelper.ProcessPaths(gamelist);

            _repository.Update(gamelist);
            return View(gamelist);
        }

 

        public IActionResult Clear(int id)
        {
            var gamelist = _repository.GetById(id);
            gamelist.Games.Clear();
            gamelist.GameFolders.Clear();
            _repository.Update(gamelist);
            return RedirectToAction("Open", new { id = gamelist.Id });

        }

        public IActionResult Import(int id)
        {
            var gamelist = _repository.GetById(id);
            if (!System.IO.File.Exists(gamelist.Path))
            {
                ViewData["Error"] = "gamelist.xml not found at " + gamelist.Path;
                var list = _repository.GetAll();
                return View("Index", list);
            }
            var games = GamelistHelper.GetGames(gamelist.Path);
            var folders = GamelistHelper.GetFolders(gamelist.Path);
            gamelist.Games = games.ToList();
            gamelist.GameFolders = folders.ToList();
            _repository.Update(gamelist);
            return RedirectToAction("Open", new { id = gamelist.Id });
        }
    }
}