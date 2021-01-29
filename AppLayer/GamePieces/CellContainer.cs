using System;
using System.Collections.Generic;

namespace AppLayer.GamePieces
{
    public class CellContainer
    {
        public string ContainerType { get; set; }
        public Gameboard ParentGameboard { get; set; }
        public int ContainerNumber { get; set; }
        public List<Cell> Cells { get; set; }
        public List<char> UsedSymbols { get; set; }

        public CellContainer(string type, Gameboard gameboard, int num)
        {
            ContainerType = type;
            ParentGameboard = gameboard;
            ContainerNumber = num;
            Cells = new List<Cell>();
            UsedSymbols = new List<char>();

            for (int i = 0; i < ParentGameboard.n; i++)
                Cells.Add(new Cell());
        }

        public bool IsInitiallyValid()
        {
            List<char> symbolCheckList = new List<char>();
            foreach(char symbol in UsedSymbols)
            {
                if (symbolCheckList.Contains(symbol) && !symbol.Equals('-'))
                {
                    Console.WriteLine("Invalid puzzle: " + ContainerType + " " + ContainerNumber + " has multiples of symbol: " + symbol);
                    return false;
                }
                else
                    symbolCheckList.Add(symbol);
            }

            return true;
        }

        public override string ToString()
        {
            string str = "";
            foreach (Cell cell in Cells)
                str += cell.Symbol + " ";
            str += "\r\n";
            return str;
        }
    }
}
