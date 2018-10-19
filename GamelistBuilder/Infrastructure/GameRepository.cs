using GamelistBuilder.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamelistBuilder.Infrastructure
{
    public class GameRepository : Repository<Game>
    {
        public GameRepository(GamelistBuilderContext context) : base(context)
        {
        }

        public override Game GetById(int id)
        {

            return _context.Games.Single(g => g.Id == id);
            //_context.Gamelists. Single(e => e.Id == id).
        }
    }
}
