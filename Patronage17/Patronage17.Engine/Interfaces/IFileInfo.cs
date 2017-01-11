using System;
using System.Collections.Generic;
using System.Text;

namespace Patronage17.Engine.Interfaces
{
    public interface IFileInfo
    {
        string Name { get; set; }
        string CreationTime { get; set; }
        string LastWriteTime { get; set; }
        string Extension { get; set; }
    }
}
