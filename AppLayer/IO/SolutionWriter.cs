using System;
using AppLayer.GamePieces;

namespace AppLayer.IO
{
    public abstract class SolutionWriter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public abstract void Write(string filename, string results);
    }
}
