using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Models
{
    public class Gamelist : BaseModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Platform Platform { get; set; }
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
        public IList<Game> Games { get; set; }
        public IList<GameFolder> Folders { get; set; }

    }
}
