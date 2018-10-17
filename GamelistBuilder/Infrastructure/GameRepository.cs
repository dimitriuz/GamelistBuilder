using GamelistBuilder.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamelistBuilder.Infrastructure
{
    public class GameRepository : IXMLRepository<Game>
    {
        private readonly string _path;

        public GameRepository(string path)
        {
            _path = path;
        }

        public XDocument GetXMLFromFile(string path)
        {
            return XDocument.Load(path);
        }

        public IQueryable<Game> GetRows(string path)
        {
            return GetGamelistRowFromXML(GetXMLFromFile(path));
        }

        public IQueryable<Game> GetGamelistRowFromXML(XDocument xml)
        {
            var games = from xe in xml.Element("gameList").Elements("game")
                        select getGameFromElement(xe);
            var folders = from xe in xml.Element("gameList").Elements("folder")
                        select getFolderFromElement(xe);

            return games.Concat(folders).OrderBy(n => n.IsFolder).AsQueryable();
        }

        private Game getGameFromElement(XElement el)
        {
            Game game = new Game();
            game.Name = GetElement(el, "name");
            game.Id = GetAttribute(el, "id");
            game.Source = GetAttribute(el, "source");
            game.Path = GetElement(el, "path");
            game.Desc = GetElement(el, "desc");
            game.IsFolder = true;

            var str_rating = GetElement(el, "rating");
            if (!string.IsNullOrWhiteSpace(str_rating)) {
                bool success = float.TryParse(str_rating, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out float rating);
                if (success)
                    game.Rating = rating;
            }
            
            var str_date = GetElement(el, "releasedate");
            if (!string.IsNullOrWhiteSpace(str_date))
            {
                bool success = DateTime.TryParse(str_date, out DateTime releasedate);
                if (success)
                    game.ReleaseDate = releasedate;
            }

            game.Developer = GetElement(el, "developer");
            game.Publisher = GetElement(el, "publisher");
            game.Genre = GetElement(el, "genre");
            game.Players = GetElement(el, "players");
            game.Hash = GetElement(el, "hash");
            game.Image = GetElement(el, "image");
            game.Marquee = GetElement(el, "marquee");
            game.Video = GetElement(el, "video");

            var str_favorite = GetElement(el, "favorite");
            if (!string.IsNullOrWhiteSpace(str_favorite))
            {
                bool success = bool.TryParse(str_favorite, out bool favorite);
                if (success)
                    game.Favorite = favorite;
            }
            return game;
        }

        private Game getFolderFromElement(XElement el)
        {
            Game game = new Game();
            game.Name = GetElement(el, "name");
            game.Path = GetElement(el, "path");
            game.Desc = GetElement(el, "desc");

            var str_rating = GetElement(el, "rating");
            if (!string.IsNullOrWhiteSpace(str_rating))
            {
                bool success = float.TryParse(str_rating, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out float rating);
                if (success)
                    game.Rating = rating;
            }

            game.Image = GetElement(el, "image");
            return game;
        }

        private string GetAttribute(XElement el, string attr)
        {
            var val = el.Attribute(attr);
            if (val != null)
                return val.Value;
            return "";
        }
        private string GetElement(XElement el, string attr)
        {
            var val = el.Element(attr);
            if (val != null)
                return val.Value;
            return "";
        }


        public IQueryable<Game> GetAll()
        {
            return GetRows(_path);
        }

        public void Create(Game entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Game entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Game entity)
        {
            throw new NotImplementedException();
        }

        public Game GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
