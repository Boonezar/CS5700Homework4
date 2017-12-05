using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.GamePieces
{
    public class Cell
    {
        public char Symbol { get; set; }
        public CellContainer ParentBlock { get; set; }
        public CellContainer ParentColumn { get; set; }
        public CellContainer ParentRow { get; set; }
        public List<char> PossibleSymbols { get; set; }

        public void FindPossibleSymbols()
        {
            List<char> usedSymbols = new List<char>();
            List<char> possibleSymbols = new List<char>();

            foreach (char symbol in ParentBlock.UsedSymbols)
                if (!usedSymbols.Contains(symbol))
                    usedSymbols.Add(symbol);
            foreach (char symbol in ParentColumn.UsedSymbols)
                if (!usedSymbols.Contains(symbol))
                    usedSymbols.Add(symbol);
            foreach (char symbol in ParentRow.UsedSymbols)
                if (!usedSymbols.Contains(symbol))
                    usedSymbols.Add(symbol);

            foreach (char symbol in ParentBlock.ParentGameboard.Symbols)
                if (!usedSymbols.Contains(symbol))
                    possibleSymbols.Add(symbol);

            PossibleSymbols = possibleSymbols;
        }
    }
}
