using GamelistBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamelistBuilder.Helpers
{
    public static class PlatformHelper
    {
        public static IQueryable<Platform> ImportPlatforms(string path)
        {
            return GetListFromXML(XMLHelper.GetXMLFromFile(path));
        }

        private static IQueryable<Platform> GetListFromXML(XDocument xml)
        {
            var items = from xe in xml.Element("systemList").Elements("system")
                        select new Platform
                        {
                            Name = xe.Element("name").Value,
                            FullName = xe.Element("fullname").Value,
                            Extensions = xe.Element("extension").Value
                        };
            return items.AsQueryable();
        }

        public static IQueryable<string> GetExtensions(Platform platform)
        {
            return platform.Extensions.Split(' ').AsQueryable();
        }
    }
}
