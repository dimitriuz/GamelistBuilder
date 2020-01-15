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
using System.Timers;
using System.Diagnostics;

namespace GamelistBuilder.Controllers
{
    public class GamelistController : Controller
    {
        private IRepository<Gamelist> _repository;
        private IRepository<Platform> _platformsRepository;
        private IRepository<Game> _gamesRepository;
        private IRepository<GameFolder> _foldersRepository;

        public GamelistController(IRepository<Gamelist> repository, IRepository<Platform> platforms, IRepository<Game> games, IRepository<GameFolder> folders)
        {
            _repository = repository;
            _platformsRepository = platforms;
            _gamesRepository = games;
            _foldersRepository = folders;
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
            var data = new GamelistViewModel();
            var platforms = _platformsRepository.GetAll();
            ViewBag.Platforms = new SelectList(platforms, "Id", "Name");
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(GamelistViewModel data)
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _repository.GetById(id);
            var dataVM = new GamelistViewModel
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _repository.GetById(id);
            _repository.Delete(data);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Open (int id)
        {
            var gamelist = _repository.GetById(id);
            GamelistHelper.ProcessPaths(gamelist);
            _repository.Update(gamelist);
            var UnusedImages = GamelistHelper.GetUnusedMedia(gamelist, GamelistMediaType.Image);
            var UnusedVideo = GamelistHelper.GetUnusedMedia(gamelist, GamelistMediaType.Video);
            var UnusedMarquee = GamelistHelper.GetUnusedMedia(gamelist, GamelistMediaType.Marque);
            var NewRoms = GamelistHelper.GetNewRoms(gamelist);
            ViewData["UnusedImages"] = UnusedImages.Count();
            ViewData["UnusedVideo"] = UnusedVideo.Count();
            ViewData["UnusedMarquee"] = UnusedMarquee.Count();
            ViewData["NewRoms"] = NewRoms.Count();


            return View(gamelist);
        }

        [HttpGet]
        public IActionResult Clear(int id)
        {
            var gamelist = _repository.GetById(id);
            gamelist.Games.Clear();
            gamelist.GameFolders.Clear();
            _repository.Update(gamelist);
            return RedirectToAction("Open", new { id = gamelist.Id });

        }

        [HttpGet]
        public IActionResult Import(int id)
        {
            var gamelist = _repository.GetById(id);
            if (!System.IO.File.Exists(gamelist.Path))
            {
                ViewData["Error"] = "gamelist.xml not found at " + gamelist.Path;
                var list = _repository.GetAll();
                return View("Index", list);
            }
            var folders = GamelistHelper.GetFolders(gamelist.Path).ToList();
            folders.ForEach(f => 
            {
                f.Gamelist = gamelist;
                _foldersRepository.Create(f);
            });

            var games = GamelistHelper.GetGames(gamelist.Path).ToList();

            gamelist.Games = games;
            gamelist.GameFolders = folders;

            _repository.Update(gamelist);
            
            return RedirectToAction("Open", new { id = gamelist.Id });
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

        [HttpGet]
        public IActionResult EditGame(int id)
        {
            var data = _gamesRepository.GetById(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult DeleteFolder(int id)
        {
            var folder = _foldersRepository.GetById(id);

            //clear gamelist from folder
            var gamelist = folder.Gamelist;
            gamelist.GameFolders.Remove(folder);
            _repository.Update(gamelist);

            //clear games from folder
            var games = _gamesRepository.Find(g => g.GameFolder == folder).ToList();
            foreach (var game in games)
            {
                game.GameFolder = null;
                _gamesRepository.Update(game);
            };

            //delete folder
            _foldersRepository.Delete(folder);

            return RedirectToAction("Open", new { id = gamelist.Id });
        }

        [HttpGet]
        public IActionResult DeleteUnusedMedia(int id)
        {
            var gamelist = _repository.GetById(id);
            GamelistHelper.DeleteUnusedMedia(gamelist);

            return RedirectToAction("Open", new { id = gamelist.Id });
        }

        [HttpGet]
        public IActionResult ImportNewRoms(int id)
        {
            var gamelist = _repository.GetById(id);
            GamelistHelper.ImportNewRoms(gamelist);

            return RedirectToAction("Open", new { id = gamelist.Id });
        }

        [HttpGet]
        public IActionResult DeleteGame(int id)
        {
            var data = _gamesRepository.GetById(id);
            var gamelistId = data.Gamelist.Id;
            _gamesRepository.Delete(data);
            return RedirectToAction("Open", new { id = gamelistId });
        }

    }
}