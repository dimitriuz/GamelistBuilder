using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Runtime.InteropServices;
using GamelistBuilder.Infrastructure;
using GamelistBuilder.ViewModels;

namespace GamelistBuilder.Controllers
{
    public class FolderController : Controller
    {
        public IActionResult Index(string path)
        {

            var drives = FolderRepository.GetDrives();
            if (string.IsNullOrWhiteSpace(path)) {
                if (drives.Count() > 0)
                {
                    path = drives.ElementAt(0);
                }
                else
                {
                    path = "/home/";
                }
            };
            var files = FolderRepository.GetFiles(path);
            var directories = FolderRepository.GetDirectories(path);

            var result = new FolderViewModel
            {
                drives = drives,
                files = files,
                directories = directories,
                path = path
            };
            return View(result);
        }

        public IActionResult Up(string path)
        {
            path = FolderRepository.GetUpPath(path);
            return RedirectToAction("Index", new { path });
        }
    }
}