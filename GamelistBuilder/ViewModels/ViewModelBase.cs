using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.ViewModels
{
    public class ViewModelBase
    {
        public string Title { get; set; }

        public ViewModelBase(string title = "")
        {
            Title = title;
        }
    }
}
