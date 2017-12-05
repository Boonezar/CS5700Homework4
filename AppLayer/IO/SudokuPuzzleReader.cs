using System;
using AppLayer.GamePieces;

namespace AppLayer.IO
{
    public abstract class SudokuPuzzleReader
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public abstract Gameboard Read(string filename);        
    }
}
