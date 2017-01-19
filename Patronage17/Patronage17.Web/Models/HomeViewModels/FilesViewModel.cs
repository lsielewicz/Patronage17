using Patronage17.Engine.Interfaces;
using System.Collections.Generic;

namespace Patronage17.Web.Models.HomeViewModels
{
    public class FilesViewModel
    {
        public string Directory { get; set; }
        public Dictionary<string, IFileInfo> Files { get; set; }
        
        public FilesViewModel()
        {
            this.Files = new Dictionary<string, IFileInfo>();
        }
    }
}
