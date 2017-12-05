using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLayer.IO;
using AppLayer.GamePieces;

namespace UnitTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ColumnsAndRowsMatch()
        {
            SudokuPuzzleReader reader = new SudokuPuzzleTextReader();
            Gameboard gameboard = reader.Read("../../../SamplePuzzles/Input/Puzzle-9x9-0001.txt");
            for(int i = 0; i < gameboard.n; i++)
            {
                for(int j = 0; j < gameboard.n; j++)
                {
                    Assert.AreEqual(gameboard.Columns[i].Cells[j], gameboard.Rows[j].Cells[i]);
                }
            }
        }

        [TestMethod]
        public void  RowsAndBlocksMatch()
        {
            SudokuPuzzleReader reader = new SudokuPuzzleTextReader();
            Gameboard gameboard = reader.Read("../../../SamplePuzzles/Input/Puzzle-4x4-0001.txt");
            Assert.AreEqual(gameboard.Rows[0].Cells[0], gameboard.Blocks[0].Cells[0]);
            Assert.AreEqual(gameboard.Rows[0].Cells[1], gameboard.Blocks[0].Cells[1]);
            Assert.AreEqual(gameboard.Rows[0].Cells[2], gameboard.Blocks[1].Cells[0]);
            Assert.AreEqual(gameboard.Rows[0].Cells[3], gameboard.Blocks[1].Cells[1]);
            Assert.AreEqual(gameboard.Rows[1].Cells[0], gameboard.Blocks[0].Cells[2]);
            Assert.AreEqual(gameboard.Rows[1].Cells[1], gameboard.Blocks[0].Cells[3]);
            Assert.AreEqual(gameboard.Rows[1].Cells[2], gameboard.Blocks[1].Cells[2]);
            Assert.AreEqual(gameboard.Rows[1].Cells[3], gameboard.Blocks[1].Cells[3]);
            Assert.AreEqual(gameboard.Rows[2].Cells[0], gameboard.Blocks[2].Cells[0]);
            Assert.AreEqual(gameboard.Rows[2].Cells[1], gameboard.Blocks[2].Cells[1]);
            Assert.AreEqual(gameboard.Rows[2].Cells[2], gameboard.Blocks[3].Cells[0]);
            Assert.AreEqual(gameboard.Rows[2].Cells[3], gameboard.Blocks[3].Cells[1]);
            Assert.AreEqual(gameboard.Rows[3].Cells[0], gameboard.Blocks[2].Cells[2]);
            Assert.AreEqual(gameboard.Rows[3].Cells[1], gameboard.Blocks[2].Cells[3]);
            Assert.AreEqual(gameboard.Rows[3].Cells[2], gameboard.Blocks[3].Cells[2]);
            Assert.AreEqual(gameboard.Rows[3].Cells[3], gameboard.Blocks[3].Cells[3]);
        }
    }
}
