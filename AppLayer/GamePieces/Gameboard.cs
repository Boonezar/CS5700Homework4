using System;
using System.Collections.Generic;

namespace AppLayer.GamePieces
{
    public class Gameboard
    {
        public int n { get; set; }
        public List<char> Symbols { get; set; }
        public readonly Dictionary<string, List<CellContainer>> Containers;
        public bool IsValidGame { get; set; }
        
        public Gameboard(int size, List<char> symbols, string filename)
        {
            n = size;
            Symbols = symbols;
            Containers = new Dictionary<string, List<CellContainer>>()
            {
                { "Blocks", new List<CellContainer>() },
                { "Columns", new List<CellContainer>() },
                { "Rows", new List<CellContainer>() }
            };
            IsValidGame = true;

            for (int i = 0; i < n; i++)
            {
                Containers["Blocks"].Add(new CellContainer("Block",this, i));
                Containers["Columns"].Add(new CellContainer("Column", this, i));
                Containers["Rows"].Add(new CellContainer("Row", this, i));
            }
        }

        public bool IsInitialBoardValid()
        {
            foreach(KeyValuePair<string, List<CellContainer>> section in Containers)
               foreach(CellContainer container in section.Value)
                    if (!container.IsInitiallyValid())
                        return false;
            return true;
        }

        public bool IsPuzzleSolved()
        {
            foreach (CellContainer container in Containers["Rows"])
                foreach (Cell cell in container.Cells)
                    if (cell.Symbol == '-')
                        return false;
            return true;
        }


        public string ToString(bool firstPrint = false)
        {
            string str = "\r\n";

            if (!IsValidGame)
                str += "Not a valid sudoku puzzle.\n\r";
            if (firstPrint)
            {
                str += $"{n}\r\n";
                foreach (char symbol in Symbols)
                    str += $"{symbol} ";
                str += "\r\n";
            }
            else if (IsPuzzleSolved())
                str += "SOLVED!\r\n";
            else
                str += "Not solved...\r\n";
            
            foreach (CellContainer row in Containers["Rows"])
                str += row.ToString();

            return str;
        }
    }
}
