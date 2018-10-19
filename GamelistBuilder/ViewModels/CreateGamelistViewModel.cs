using GamelistBuilder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.ViewModels
{
    public class CreateGamelistViewModel
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

        //public ICollection<Platform> Platforms { get; set; }
        public int PlatformId { get; set; }
    }
}
