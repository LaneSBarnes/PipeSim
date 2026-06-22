using System;
using System.Collections.Generic;
using System.Text;

namespace PipeSim.Model
{
    public class CellGrid
    {
        public CellGrid(Cell[,]? cells = null)
        {
            Cells = cells ?? new Cell[0, 0];
        }

        public Cell[,] Cells { get; set; } = new Cell[0, 0];
    }
}
