using System;
using AppLayer.GamePieces;

namespace AppLayer.IO
{
    public class SolutionConsoleWriter : SolutionWriter
    {
        public override void Write(string filename, string results)
        {
            Console.WriteLine(results);
        }
    }
}