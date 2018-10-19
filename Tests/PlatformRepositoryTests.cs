using NUnit.Framework;
using GamelistBuilder.Infrastructure;
using System.Xml.Linq;
using System.Linq;
using GamelistBuilder.Models;
using GamelistBuilder.Helpers;
using System.IO;

namespace Tests
{
    [TestFixture]
    public class PlatformRepositoryTests
    {
        //PlatformRepository _repository;
        [SetUp]
        public void Setup()
        {
            //_repository = new PlatformHelper();
        }

        [Test]
        public void TestGetListFromXML()
        {
            XDocument xdoc = XDocument.Parse(@"<?xml version=""1.0""?>
            <systemList>
              <system>
                <name>amiga</name>
                <fullname>Amiga</fullname>
                <extension>.adf .adz .dms .ipf .lha .sh .uae .zip .ADF .ADZ .DMS .IPF .LHA .SH .UAE .ZIP</extension>
              </system>
              <system>
                <name>amstradcpc</name>
                <fullname>Amstrad CPC</fullname>
                <extension>.cdt .cpc .dsk .CDT .CPC .DSK</extension>
              </system>
              </systemList>
            ");
            var filename = Path.GetTempFileName();
            xdoc.Save(filename);
            var result = PlatformHelper.ImportPlatforms(filename).ToList();
            Assert.AreEqual(2, result.Count(), "Count of platforms unexpected");
            Assert.AreEqual("amiga", result.First().Name, "unexpected platform entry");
            Assert.AreEqual(16, result.First().Extensions.Count(), "Count of extensions unexpected");
        }
    }
}