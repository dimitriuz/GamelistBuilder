using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Models
{
    public class Platform : BaseModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<FileExtensions> Extensions { get; set; }
        public virtual ICollection<Gamelist> Gamelists { get; set; }

    }
}
