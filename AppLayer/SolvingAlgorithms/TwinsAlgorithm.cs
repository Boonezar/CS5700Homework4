using System;
using System.Collections.Generic;
using AppLayer.GamePieces;

namespace AppLayer.SolvingAlgorithms
{
    public class TwinsAlgorithm : SudokuSolvingAlgorithmTemplate
    {
        protected override List<Cell> FindApplicableCells()
        {
            List<Cell> cells = null;
            return cells;
        }
        protected override bool ApplyAlgorithmOnCells(List<Cell> cells)
        {
            return false;
        }
        protected override void ApplyRippleEffects(List<Cell> cells)
        {

        }
    }
}
