using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Models
{
    public enum GamelistMediaType
    {
        Image,
        Video,
        Marque
    };

    public class Gamelist : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string Path { get; set; }
        [DisplayName("ROMS")]
        [Required]
        public string GamesDirectory { get; set; }
        [DisplayName("Images")]
        public string ImagesDirectory { get; set; }
        [DisplayName("Video")]
        public string VideoDirectory { get; set; }
        [DisplayName("Marque")]
        public string MarqueDirectory { get; set; }

        public virtual Platform Platform { get; set; }

        public bool Imported { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<GameFolder> GameFolders { get; set; }

    }
}
