using GamelistBuilder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamelistBuilder.Infrastructure
{
    public class GameFolderRepository : Repository<GameFolder>
    {
        public GameFolderRepository(GamelistBuilderContext context) : base(context)
        {
        }

        public override GameFolder GetById(int id)
        {
            return _context.Folders.Include(g => g.Gamelist).Single(g => g.Id == id);
        }
    }
}
