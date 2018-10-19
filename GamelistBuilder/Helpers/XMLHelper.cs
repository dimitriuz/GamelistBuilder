using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamelistBuilder.Helpers
{
    public static class XMLHelper
    {
        public static XDocument GetXMLFromFile(string path)
        {
            return XDocument.Load(path);
        }

        public static string GetAttribute(XElement el, string attr)
        {
            var val = el.Attribute(attr);
            if (val != null)
                return val.Value;
            return "";
        }
        public static string GetElement(XElement el, string attr)
        {
            var val = el.Element(attr);
            if (val != null)
                return val.Value;
            return "";
        }
    }
}
