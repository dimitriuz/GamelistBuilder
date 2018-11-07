using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using GamelistBuilder.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GamelistBuilder.Infrastructure
{
    public class GamelistRepository : Repository<Gamelist>
    {
        public GamelistRepository(GamelistBuilderContext context) : base(context) {}

        public override Gamelist GetById(int id)
        {
            return _context.Gamelists
                .Include(g => g.Games)
                .Include(g => g.GameFolders)
                .Include(g => g.Platform)
                .Include(g => g.Platform)
                .Single(g => g.Id == id);
        }
    }
}
