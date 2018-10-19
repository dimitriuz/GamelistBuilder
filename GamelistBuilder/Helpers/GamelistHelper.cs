using GamelistBuilder.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamelistBuilder.Helpers
{
    public static class GamelistHelper
    {

        public static IQueryable<Game> GetGames(string path)
        {
            return GetGamesFromXML(XMLHelper.GetXMLFromFile(path));
        }

        public static IQueryable<GameFolder> GetFolders(string path)
        {
            return GetFoldersFromXML(XMLHelper.GetXMLFromFile(path));
        }

        private static IQueryable<Game> GetGamesFromXML(XDocument xml)
        {
            var games = from xe in xml.Element("gameList").Elements("game")
                        select GetGameFromElement(xe);
            var folders = from xe in xml.Element("gameList").Elements("folder")
                          select GetFolderFromElement(xe);

            return games.AsQueryable();
        }

        private static IQueryable<GameFolder> GetFoldersFromXML(XDocument xml)
        {
            var folders = from xe in xml.Element("gameList").Elements("folder")
                          select GetFolderFromElement(xe);

            return folders.AsQueryable();
        }

        private static Game GetGameFromElement(XElement el)
        {
            Game game = new Game
            {
                Name = XMLHelper.GetElement(el, "name"),
                SourceId = XMLHelper.GetAttribute(el, "id"),
                Source = XMLHelper.GetAttribute(el, "source"),
                Path = XMLHelper.GetElement(el, "path"),
                Desc = XMLHelper.GetElement(el, "desc"),
                IsFolder = true
            };

            var str_rating = XMLHelper.GetElement(el, "rating");
            if (!string.IsNullOrWhiteSpace(str_rating))
            {
                bool success = float.TryParse(str_rating, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out float rating);
                if (success)
                    game.Rating = rating;
            }

            var str_date = XMLHelper.GetElement(el, "releasedate");
            if (!string.IsNullOrWhiteSpace(str_date))
            {
                bool success = DateTime.TryParse(str_date, out DateTime releasedate);
                if (success)
                    game.ReleaseDate = releasedate;
            }

            game.Developer = XMLHelper.GetElement(el, "developer");
            game.Publisher = XMLHelper.GetElement(el, "publisher");
            game.Genre = XMLHelper.GetElement(el, "genre");
            game.Players = XMLHelper.GetElement(el, "players");
            game.Hash = XMLHelper.GetElement(el, "hash");
            game.Image = XMLHelper.GetElement(el, "image");
            game.Marquee = XMLHelper.GetElement(el, "marquee");
            game.Video = XMLHelper.GetElement(el, "video");

            if (File.Exists(game.Image))
                game.ImageFound = true;

            var str_favorite = XMLHelper.GetElement(el, "favorite");
            if (!string.IsNullOrWhiteSpace(str_favorite))
            {
                bool success = bool.TryParse(str_favorite, out bool favorite);
                if (success)
                    game.Favorite = favorite;
            }
            return game;
        }

        private static GameFolder GetFolderFromElement(XElement el)
        {
            GameFolder folder = new GameFolder
            {
                Name = XMLHelper.GetElement(el, "name"),
                Path = XMLHelper.GetElement(el, "path"),
                Desc = XMLHelper.GetElement(el, "desc")
            };

            var str_rating = XMLHelper.GetElement(el, "rating");
            if (!string.IsNullOrWhiteSpace(str_rating))
            {
                bool success = float.TryParse(str_rating, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out float rating);
                if (success)
                    folder.Rating = rating;
            }

            folder.Image = XMLHelper.GetElement(el, "image");
            return folder;
        }

        public static void ProcessPaths(Gamelist gamelist)
        {
            var RootDir = Path.GetDirectoryName(gamelist.Path);
            var RomsDir = CombinePath(RootDir, gamelist.GamesDirectory);
            var ImagesDir = CombinePath(RootDir, gamelist.ImagesDirectory);
            var VideoDir = CombinePath(RootDir, gamelist.VideoDirectory);
            var MarqueDir = CombinePath(RootDir, gamelist.MarqueDirectory);

            var RomFiles = GetDirContent(RomsDir);
            var ImageFiles = GetDirContent(ImagesDir);
            var VideoFiles = GetDirContent(VideoDir);
            var MarqueFiles = GetDirContent(MarqueDir);

            foreach (var game in gamelist.Games)
            {
                game.RomFound = FileExists(RomFiles, RootDir, game.Path);
                game.ImageFound = FileExists(ImageFiles, RootDir, game.Image);
                game.VideoFound = FileExists(VideoFiles, RootDir, game.Video);
                game.MarqueFound = FileExists(MarqueFiles, RootDir, game.Marquee);
            };
        }

        private static string CombinePath(string root, string path)
        {
            bool relative = path[0] == '.';
            if (relative)
            {
                path = path.Substring(1);
                path = path.Replace('/', Path.DirectorySeparatorChar);
                path = path.Replace('\\', Path.DirectorySeparatorChar);
                path = root + path;
            }
            return path;
        }

        private static bool FileExists(FileInfo[] files, string path, string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return false;
            var fullname = CombinePath(path, filename);
            var file = files.FirstOrDefault(i => i.FullName == fullname);

            //path = CombinePath(root, path);

            return file != null;
        }

        private static FileInfo[] GetDirContent(string path)
        {
            var di = new DirectoryInfo(path);
            var files = di.GetFiles("*.*", SearchOption.AllDirectories);
            return files;
        }

    }
}
