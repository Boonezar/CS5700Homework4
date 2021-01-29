using System;
using System.IO;
using System.Collections.Generic;
using AppLayer.IO;
using AppLayer.GamePieces;
using AppLayer.SolvingAlgorithms;

namespace SudokuSolver
{
    static class Program
    {
        private static readonly SudokuPuzzleReader[] SudokuPuzzleReaders = new SudokuPuzzleReader[]
                        {
                            new SudokuPuzzleTextReader() { Name = ".txt", Description  = "Reads a Sudoku Puzzle from a text file." },
                        };

        private static readonly SolutionWriter[] SolutionWriters = new SolutionWriter[]
                        {
                            new SolutionConsoleWriter() { Name = "Console", Description = "Writes Sudoku Puzzle solution to the Console." },
                            new SolutionTextWriter() { Name = ".txt", Description = "Writes Sudoku Puzzle solution to the given txt file." }
                        };

        private static readonly SudokuSolvingAlgorithmTemplate[] SudokuSolvingAlgorithms = new SudokuSolvingAlgorithmTemplate[]
                        {
                            new OneAwayAlgorithm(){ Name = "One Away Alg", MyStopwatch = new System.Diagnostics.Stopwatch(), MyGameboard = null, ChangeMade = false, Counter = 0 },
                            new TwinsAlgorithm(){ Name = "Twins Alg", MyStopwatch = new System.Diagnostics.Stopwatch(), MyGameboard = null, ChangeMade = false, Counter = 0 }
                        };

        static void Main(string[] args)
        {
            //Input Validation
            if (!IsValidParameters(args))
                return;
            
            string inputFilename = args[0];
            SudokuPuzzleReader reader = GetSudokuPuzzleReader(inputFilename);
            if (!IsValidSudokuPuzzleReader(reader))
                return;

            Gameboard gameboard = reader.Read(inputFilename);
            
            string resultsToBeWritten = gameboard.ToString(true);
            if (gameboard.IsInitialBoardValid() && gameboard.IsValidGame)
            {
                RunSolvingAlgorithms(gameboard);
                resultsToBeWritten += gameboard.ToString();
                resultsToBeWritten += GetTimesFromAlgs();
            }
            
            string outputFilename = args.Length == 2 ? args[1] : null;
            SolutionWriter writer = GetSolutionWriter(outputFilename);
            writer.Write(outputFilename, resultsToBeWritten);
        }

        static void PrintCommandlineMessage()
        {
            Console.WriteLine("Valid input options:");
            Console.WriteLine("-h\t\t\t\tPrint Help Message");
            Console.WriteLine("<input file>\t\t\tProvide the input file, results to console.");
            Console.WriteLine("<input file> <output file>\tProvide the input and output file.");
        }

        static bool IsValidParameters(string[] args)
        {
            if(args.Length < 1 || args.Length > 3)
            {
                Console.WriteLine("Invalid input...");
                PrintCommandlineMessage();
                return false;
            }
            if(args[0] == "-h")
            {
                PrintCommandlineMessage();
                return false;
            }
            return true;
        }

        static bool IsValidSudokuPuzzleReader(SudokuPuzzleReader myReader)
        {
            if(myReader == null)
            {
                Console.WriteLine("Invalid Input File. Supported types:");
                foreach (SudokuPuzzleReader reader in SudokuPuzzleReaders)
                    Console.WriteLine(reader.Name);
                return false;
            }
            return true;
        }

        static SudokuPuzzleReader GetSudokuPuzzleReader(string inputFilename)
        {
            SudokuPuzzleReader result = null;
            string fileType = Path.GetExtension(inputFilename);
            foreach(SudokuPuzzleReader reader in SudokuPuzzleReaders)
            {
                if(fileType == reader.Name)
                {
                    result = reader;
                    break;
                }
            }
        return result;
        }

        static SolutionWriter GetSolutionWriter(string outputFilename)
        {
            SolutionWriter result = null;
            if (outputFilename == null)
                result = SolutionWriters[0];
            else
            {
                string fileType = Path.GetExtension(outputFilename);
                foreach (SolutionWriter writer in SolutionWriters)
                {
                    if (fileType == writer.Name)
                    {
                        result = writer;
                        break;
                    }
                }
            }
            return result;
        }

        static void SetGameboardIntoAlgorithms(Gameboard gameboard)
        {
            foreach (SudokuSolvingAlgorithmTemplate alg in SudokuSolvingAlgorithms)
                alg.MyGameboard = gameboard;
        }

        static void RunSolvingAlgorithms(Gameboard gameboard)
        {
            SetGameboardIntoAlgorithms(gameboard);

            foreach (CellContainer container in gameboard.Containers["Rows"])
                foreach (Cell cell in container.Cells)
                    cell.FindPossibleSymbols();

            for(int i = 0; i < SudokuSolvingAlgorithms.Length; i++)
            {
                SudokuSolvingAlgorithmTemplate alg = SudokuSolvingAlgorithms[i];
                alg.RunSolution();
                if (alg.ChangeMade)
                    i = -1;
                if (gameboard.IsPuzzleSolved())
                    return;
            }
        }

        static string GetTimesFromAlgs()
        {
            string results = "\n\r";
            string individualTimes = "";
            TimeSpan totalTimeSpan = new TimeSpan();
            foreach(SudokuSolvingAlgorithmTemplate alg in SudokuSolvingAlgorithms)
            {
                TimeSpan ts = alg.MyStopwatch.Elapsed;
                individualTimes += alg.Name+ "\t" + alg.Counter + "\t" + ts.ToString("c") + "\n\r";
                totalTimeSpan = totalTimeSpan.Add(ts);
            }

            results += "Total Time: " + totalTimeSpan.ToString("c") + "\n\r\n\r";
            results += "Strategy\tUses\tTime\n\r" + individualTimes;
            
            return results;
        }
    }
}
