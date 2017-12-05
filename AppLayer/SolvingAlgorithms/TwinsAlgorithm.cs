using System;
using System.Collections.Generic;
using AppLayer.GamePieces;

namespace AppLayer.SolvingAlgorithms
{
    public class TwinsAlgorithm : SudokuSolvingAlgorithmTemplate
    {
        public override List<Cell> FindApplicableCells()
        {
            List<Cell> cells = null;
            return cells;
        }
        public override bool ApplyAlgorithmOnCells(List<Cell> cells)
        {
            return false;
        }
        public override void ApplyRippleEffects(List<Cell> cells)
        {

        }
    }
}
