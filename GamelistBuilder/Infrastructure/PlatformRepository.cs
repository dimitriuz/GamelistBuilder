using GamelistBuilder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GamelistBuilder.Infrastructure
{

    public class PlatformRepository : IXMLRepository<Platform>
    {
        private readonly string _cfgPath;

        public PlatformRepository()
        {
            _cfgPath = @"assets\es_systems.cfg";
        }

        public IQueryable<Platform> GetAll()
        {
            return GetListFromXML(GetXMLFromFile(_cfgPath));
        }

        public IQueryable<Platform> GetListFromXML(XDocument xml)
        {
            var items = from xe in xml.Element("systemList").Elements("system")
                        select new Platform
                        {
                            Name = xe.Element("name").Value,
                            FullName = xe.Element("fullname").Value,
                            Extensions = xe.Element("extension").Value.Split(' ').ToList()
                        };
            return items.AsQueryable();
        }

        public XDocument GetXMLFromFile(string path)
        {
            return XDocument.Load(path);
        }

        public void Create(Platform entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Platform entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Platform entity)
        {
            throw new System.NotImplementedException();
        }

        public Platform GetById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
