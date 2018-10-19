using GamelistBuilder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GamelistBuilder.Infrastructure
{

    public class PlatformRepository : Repository<Platform>
    {
        public PlatformRepository(GamelistBuilderContext context) : base(context)
        {
        }
 
    }
}
