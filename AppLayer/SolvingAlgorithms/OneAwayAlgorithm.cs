using System;
using System.Collections.Generic;
using AppLayer.GamePieces;

namespace AppLayer.SolvingAlgorithms
{
    public class OneAwayAlgorithm : SudokuSolvingAlgorithmTemplate
    {
        public override List<Cell> FindApplicableCells()
        {
            List<Cell> cells = new List<Cell>();
            foreach(KeyValuePair<string, List<CellContainer>> kvp in MyGameboard.Containers)
                foreach(CellContainer container in kvp.Value)
                    foreach(Cell cell in container.Cells)
                        if (cell.PossibleSymbols.Count == 1 && cell.Symbol == '-')
                            cells.Add(cell);
            return cells;
        }
        public override bool ApplyAlgorithmOnCells(List<Cell> cells)
        {
            if(cells.Count == 0)
                return false;
            foreach(Cell cell in cells)
                cell.Symbol = cell.PossibleSymbols[0];
            return true;
        }
        public override void ApplyRippleEffects(List<Cell> cells)
        {
            List<int> ChangedBlockList = new List<int>();
            List<int> ChangedColumnList = new List<int>();
            List<int> ChangedRowList = new List<int>();

            foreach (Cell cell in cells)
            {
                cell.ParentBlock.UsedSymbols.Add(cell.Symbol);
                if (!ChangedBlockList.Contains(cell.ParentBlock.ContainerNumber))
                    ChangedBlockList.Add(cell.ParentBlock.ContainerNumber);
                cell.ParentColumn.UsedSymbols.Add(cell.Symbol);
                if (!ChangedColumnList.Contains(cell.ParentColumn.ContainerNumber))
                    ChangedColumnList.Add(cell.ParentColumn.ContainerNumber);
                cell.ParentRow.UsedSymbols.Add(cell.Symbol);
                if (!ChangedRowList.Contains(cell.ParentRow.ContainerNumber))
                    ChangedRowList.Add(cell.ParentRow.ContainerNumber);
            }

            foreach(int i in ChangedBlockList)
                foreach(Cell cell in MyGameboard.Containers["Blocks"][i].Cells)
                    cell.FindPossibleSymbols();

            foreach (int i in ChangedColumnList)
                foreach (Cell cell in MyGameboard.Containers["Columns"][i].Cells)
                    cell.FindPossibleSymbols();

            foreach (int i in ChangedRowList)
                foreach (Cell cell in MyGameboard.Containers["Rows"][i].Cells)
                    cell.FindPossibleSymbols();
        }
    }
}
