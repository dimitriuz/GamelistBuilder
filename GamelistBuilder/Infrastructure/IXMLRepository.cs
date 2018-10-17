using GamelistBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamelistBuilder.Infrastructure
{
    public interface IXMLRepository<T> : IRepository<T>
    {
        XDocument GetXMLFromFile(string path);
    }
}
