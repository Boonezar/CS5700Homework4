using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLayer.GamePieces;

namespace AppLayer.IO
{
    public class SudokuPuzzleTextReader : SudokuPuzzleReader
    {
        public override Gameboard Read(string filename)
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                int size = Convert.ToInt32(file.ReadLine());
                string symbolsString = file.ReadLine();
                List<char> symbols = new List<char>(symbolsString.Split(' ').Select(char.Parse));
                Gameboard gameboard = new Gameboard(size, symbols, filename);

                //Checking input validity
                if (!IsSizePerferctSquare(gameboard))
                    return gameboard;

                int rowNum = 0;
                string line;
                while (!String.IsNullOrWhiteSpace(line = file.ReadLine()))
                {
                    //Checking input validity
                    if (!isValidRowNumber(gameboard, rowNum))
                        return gameboard;

                    List<char> lineSymbols = new List<char>(line.Split(' ').Select(char.Parse));
                    int columnNum = 0;
                    foreach (char symbol in lineSymbols)
                    {
                        //Checking input validity
                        isValidColumnNumber(gameboard, columnNum);
                        isValidSymbol(gameboard, symbols, symbol);

                        Cell newCell = new Cell()
                        {
                            Symbol = symbol,
                            PossibleSymbols = getNewSymbolList(symbols),
                            ParentRow = gameboard.Containers["Rows"][rowNum],
                            ParentColumn = gameboard.Containers["Columns"][columnNum]
                        };
                        gameboard.Containers["Rows"][rowNum].Cells[columnNum] = newCell;
                        gameboard.Containers["Rows"][rowNum].UsedSymbols.Add(newCell.Symbol);
                        gameboard.Containers["Columns"][columnNum].Cells[rowNum] = newCell;
                        gameboard.Containers["Columns"][columnNum].UsedSymbols.Add(newCell.Symbol);
                        columnNum++;
                    }
                    rowNum++;
                }
                file.Close();

                fillBlocks(gameboard);

                return gameboard;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error opening file: " + filename + "\n" + e.ToString());
                return new Gameboard(4, new List<char>(), filename) { IsValidGame = false };
            }
        }

        void fillBlocks(Gameboard gameboard)
        {
            int blockNum = 0;
            int sqrt = Convert.ToInt32(Math.Sqrt(gameboard.n));
            for (int col = 0; col < sqrt; col++)
            {
                for (int row = 0; row < sqrt; row++)
                {
                    int cellNum = 0;
                    for (int y = col*sqrt; y < (col+1)*sqrt; y++)
                    {
                        for(int x = row*sqrt; x < (row+1)*sqrt; x++)
                        {
                            gameboard.Containers["Blocks"][blockNum].Cells[cellNum] = gameboard.Containers["Rows"][y].Cells[x];
                            gameboard.Containers["Blocks"][blockNum].Cells[cellNum].ParentBlock = gameboard.Containers["Blocks"][blockNum];
                            gameboard.Containers["Blocks"][blockNum].UsedSymbols.Add(gameboard.Containers["Rows"][y].Cells[x].Symbol);
                            cellNum++;
                        }
                    }
                    blockNum++;
                }
            }
        }

        public bool IsSizePerferctSquare(Gameboard gameboard)
        {
            double sqrt = Math.Sqrt(gameboard.n);
            if (!(sqrt % 1 == 0))
            {
                Console.WriteLine("Input Size not a Perfect Square: n=" + gameboard.n + ", sqrt(n)=" + sqrt);
                gameboard.IsValidGame = false;
                return false;
            }
            return true;
        }

        bool isValidRowNumber(Gameboard gameboard, int rowNum)
        {
            if (rowNum == gameboard.n)
            {
                Console.WriteLine("Invalid number of rows: n=" + gameboard.n + ", rows=" + rowNum);
                gameboard.IsValidGame = false;
                return false;
            }
            return true;
        }

        bool isValidColumnNumber(Gameboard gameboard, int columnNum)
        {
            if(columnNum == gameboard.n)
            {
                Console.WriteLine("Invalid number of columns: n=" + gameboard.n + ", columns= " + columnNum);
                gameboard.IsValidGame = false;
                return false;
            }
            return true;
        }

        bool isValidSymbol(Gameboard gameboard, List<char> symbols, char symbol)
        {
            if (!symbols.Contains(symbol) && !symbol.Equals('-'))
            {
                Console.WriteLine("Invalid symbol found: " + symbol);
                gameboard.IsValidGame = false;
                return false;
            }
            return true;
        }

        List<char> getNewSymbolList(List<char> symbols)
        {
            List<char> newSymbolList = new List<char>();
            foreach (char symbol in symbols)
                newSymbolList.Add(symbol);
            return newSymbolList;
        }
    }
}
