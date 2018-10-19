using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Models
{
    public class GameFolder : BaseModel
    {
        public float Rating { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Game> Games {get; set; }

        public virtual Gamelist Gamelist { get; set; }

    }
}
