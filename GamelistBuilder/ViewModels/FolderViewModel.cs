using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.ViewModels
{
    public class FolderViewModel : ViewModelBase
    {
        public IEnumerable<string> drives { get; set; }
        public IEnumerable<string> files { get; set; }
        public IEnumerable<string> directories { get; set; }
        public string path;

    }
}
