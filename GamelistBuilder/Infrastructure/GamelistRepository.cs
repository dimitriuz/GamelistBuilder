using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using GamelistBuilder.Models;
using Newtonsoft.Json;

namespace GamelistBuilder.Infrastructure
{
    public class GamelistRepository : IRepository<Gamelist>
    {
        private readonly string dataFile;
        private readonly IList<Gamelist> data;

        public GamelistRepository()
        {
            dataFile = @"assets\gamelist.json";
            if (!File.Exists(dataFile))
            {
                data = new List<Gamelist>();
                WriteData();
            }
            else
            {
                data = JsonConvert.DeserializeObject<IList<Gamelist>>(File.ReadAllText(dataFile));
            }
        }

        public void Create(Gamelist entity)
        {
            lock (this)
            {
                // Make sure that only one thread is updating the file
                entity.Id = Guid.NewGuid().ToString();
                data.Add(entity);
                WriteData();
            }
        }

        public void Update(Gamelist entity)
        {
            var index = data.IndexOf(data.First(n => n.Id == entity.Id));
            data[index] = entity;
            WriteData();

        }

        public void Delete(Gamelist entity)
        {
            var index = data.IndexOf(data.First(n => n.Id == entity.Id));
            data.RemoveAt(index);
            WriteData();
        }

        public IQueryable<Gamelist> GetAll()
        {
            return data.AsQueryable();
        }

        public Gamelist GetById(string id)
        {
            Guid.NewGuid().ToString();
            return data.First(n => n.Id == id);
        }

        public void WriteData()
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(dataFile, json);
        }

    }
}
