using System;
using System.Collections.Generic;
using System.Diagnostics;
using AppLayer.GamePieces;

namespace AppLayer.SolvingAlgorithms
{
    public abstract class SudokuSolvingAlgorithmTemplate
    {
        public string Name { get; set; }
        public Stopwatch MyStopwatch { get; set; }
        public Gameboard MyGameboard { get; set; }
        public bool ChangeMade { get; set; }
        public int Counter { get; set; }
        protected abstract List<Cell> FindApplicableCells();
        protected abstract bool ApplyAlgorithmOnCells(List<Cell> cells);
        protected abstract void ApplyRippleEffects(List<Cell> cells);

        public void RunSolution()
        {
            ChangeMade = false;

            Counter++;
            MyStopwatch.Start();
            List<Cell> resultCells = FindApplicableCells();
            if (ApplyAlgorithmOnCells(resultCells))
            {
                ChangeMade = true;
                ApplyRippleEffects(resultCells);
            }
            MyStopwatch.Stop();
        }
    }
}
