using System;
using AppLayer.GamePieces;

namespace AppLayer.IO
{
    public class SolutionTextWriter : SolutionWriter
    {
        public override void Write(string filename, string results)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
            file.Write(results);
            file.Close();
        }
    }
}
