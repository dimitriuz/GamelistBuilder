using GamelistBuilder.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Crc32;
using System.IO;
using GamelistBuilder.Helpers;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;

namespace GamelistBuilder.Infrastructure
{
    public class ScraperSS
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public ScraperSS(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        public Game ScrapeGame(Game game)
        {
            if (game == null)
                return null;

            string ss_id = _configuration.GetValue<string>("SS_ID");
            string ss_name = _configuration.GetValue<string>("SS_Name");
            string ss_Password = _configuration.GetValue<string>("SS_Password");

            string url = @"https://www.screenscraper.fr/api/jeuInfos.php?devid={0}&devpassword={1}&softname={2}&output=json&crc={3}&md5={4}&sh1={5}&romtype=rom&romnom={6}&romtaille={7}";
            FileInfo file = new FileInfo(GamelistHelper.CombinePath(game.Gamelist.Path,game.Path));
            if (!file.Exists)
                return game;
            var crc32 = getCRC32(file.FullName);
            var md5 = getMD5(file.FullName);
            var sha1 = getSHA1(file.FullName);
            var filesize = file.Length;
            url = string.Format(url, ss_id, ss_Password, ss_name, crc32, md5, sha1, file.Name, filesize);

            var response =  GetJson(url);


            return game;

        }

        private async Task<string> GetJson(string url)
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(url);
            string result = await client.GetStringAsync("/");
            return result;
        }

        private static uint getCRC32(string file)
        {
            var crc = 0u;
            using (var f = File.OpenRead(file))
            {
                var buffer = new byte[65536];
                while (true)
                {
                    var count = f.Read(buffer, 0, buffer.Length);
                    if (count == 0)
                        break;
                    crc = Crc32Algorithm.Append(crc, buffer, 0, count);
                }
            }
            return crc;
        }

        private static string getMD5(string file)
        {
            var md5 = MD5.Create();
            using (var f = File.OpenRead(file))
            {
                var result = md5.ComputeHash(f);
                return Encoding.ASCII.GetString(result);
            }
        }

        private static string getSHA1(string file)
        {
            var sha1 = SHA1.Create();
            using (var f = File.OpenRead(file))
            {
                var result = sha1.ComputeHash(f);
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}
