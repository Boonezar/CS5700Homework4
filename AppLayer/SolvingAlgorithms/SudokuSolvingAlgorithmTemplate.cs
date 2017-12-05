using System;
using System.Collections.Generic;
using System.Diagnostics;
using AppLayer.GamePieces;

namespace AppLayer.SolvingAlgorithms
{
    public abstract class SudokuSolvingAlgorithmTemplate
    {
        public Stopwatch MyStopwatch { get; set; }
        public Gameboard MyGameboard { get; set; }
        public bool ChangeMade { get; set; }
        public void StartTimer() { MyStopwatch.Start(); }
        public void StopTimer() { MyStopwatch.Stop(); }
        public abstract List<Cell> FindApplicableCells();
        public abstract bool ApplyAlgorithmOnCells(List<Cell> cells);
        public abstract void ApplyRippleEffects(List<Cell> cells);
    }
}
