using GamelistBuilder.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GamelistBuilder.Infrastructure
{
    public class GameRepository : Repository<Game>
    {
        public GameRepository(GamelistBuilderContext context) : base(context)
        {
        }

        public override Game GetById(int id)
        {
            return _context.Games.Include(g => g.Gamelist).Single(g => g.Id == id);
        }
    }
}
