using Patronage17.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patronage17.Engine.Models
{
    public class CustomFileInfo : IFileInfo
    {
        public string Name { get; set; }
        public string CreationTime { get; set; }
        public string LastWriteTime { get; set; }
        public string Extension { get; set; }
    }
}
