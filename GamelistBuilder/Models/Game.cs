using System;

namespace GamelistBuilder.Models
{
    public class Game : BaseModel
    {
        public string SourceId { get; set; }
        public string Source { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public float Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public string Players { get; set; }
        public string Hash { get; set; }
        public string Image { get; set; }
        public string Marquee { get; set; }
        public string Video { get; set; }
        public bool Favorite { get; set; }

        public bool IsFolder { get; set; }
        public bool RomFound { get; set; }
        public bool ImageFound { get; set; }
        public bool VideoFound { get; set; }
        public bool MarqueFound { get; set; }

        public virtual Gamelist Gamelist { get; set; }
        
        public GameFolder GameFolder { get; set; }
        public int? GameFolderId { get; set; }

    }
}
