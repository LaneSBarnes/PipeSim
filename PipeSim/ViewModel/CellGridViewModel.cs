using System.Diagnostics;
using System.Windows.Media;
using PipeSim.Model;

namespace PipeSim.ViewModel
{
    class CellGridViewModel : ViewModelBase
    {
        const int timeStepMs = 1000;

        private CellGrid _cellGrid;

        public CellGridViewModel()
        {
            _cellGrid = new CellGrid();

            LoadCellGrid();
        }

        public CellGrid CellGrid
        {
            get => _cellGrid;
            set
            {
                if (_cellGrid != value)
                {
                    _cellGrid = value;
                    OnPropertyChanged();
                }
            }
        }

        public void UpdateCellGrid()
        {
            int colCount = _cellGrid.Cells.GetLength(0);
            int rowCount = _cellGrid.Cells.GetLength(1);
            float[,] newFlowGrid = GetWaterLevels();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    Cell currentCell = _cellGrid.Cells[i, j];

                    float rightFlow = 0;
                    float downFlow = 0;
                    float leftFlow = 0;
                    float upFlow = 0;

                    if (i + 1 < colCount)
                    {
                        Cell rightCell = _cellGrid.Cells[i + 1, j];
                        rightFlow = CalculatePossibleFlow(currentCell, rightCell);
                    }

                    if (j + 1 < rowCount)
                    {
                        Cell downCell = _cellGrid.Cells[i, j + 1];
                        downFlow = CalculatePossibleFlow(currentCell, downCell);
                    }

                    if (i - 1 >= 0)
                    {
                        Cell leftCell = _cellGrid.Cells[i - 1, j];
                        leftFlow = CalculatePossibleFlow(currentCell, leftCell);
                    }

                    if (j - 1 >= 0)
                    {
                        Cell upCell = _cellGrid.Cells[i, j - 1];
                        upFlow = CalculatePossibleFlow(currentCell, upCell);
                    }

                    float totalOutFlow = rightFlow + downFlow + leftFlow + upFlow;

                    if (totalOutFlow > currentCell.WaterLevel && totalOutFlow > 0)
                    {
                        float reductionRate = currentCell.WaterLevel / totalOutFlow;

                        rightFlow = rightFlow * reductionRate;
                        downFlow = downFlow * reductionRate;
                        leftFlow = leftFlow * reductionRate;
                        upFlow = upFlow * reductionRate;
                    }

                    if (i + 1 < colCount)
                    {
                        newFlowGrid[i, j] -= rightFlow;
                        newFlowGrid[i + 1, j] += rightFlow;
                    }

                    if (j + 1 < rowCount)
                    {
                        newFlowGrid[i, j] -= downFlow;
                        newFlowGrid[i, j + 1] += downFlow;
                    }

                    if (i - 1 >= 0)
                    {
                        newFlowGrid[i, j] -= leftFlow;
                        newFlowGrid[i - 1, j] += leftFlow;
                    }

                    if (j - 1 >= 0)
                    {
                        newFlowGrid[i, j] -= upFlow;
                        newFlowGrid[i, j - 1] += upFlow;
                    }
                }
            }

            SetWaterLevels(newFlowGrid);
        }

        private float CalculatePossibleFlow(Cell outputCell, Cell inputCell)
        {
            float possibleFlowOut = Math.Min(outputCell.WaterLevel, outputCell.MaxFlowRate);
            float possibleFlowIn = Math.Min(inputCell.Capacity - inputCell.WaterLevel, inputCell.MaxFlowRate);

            float flow = Math.Min(possibleFlowOut, possibleFlowIn);

            float gradientForce = 0f;
            if (inputCell.WaterLevel < outputCell.WaterLevel && outputCell.WaterLevel > 0)
            {
                gradientForce = 1 - (inputCell.WaterLevel / outputCell.WaterLevel);
            }

            return flow * gradientForce;
        }

        private float[,] GetWaterLevels()
        {
            int colCount = _cellGrid.Cells.GetLength(0);
            int rowCount = _cellGrid.Cells.GetLength(1);
            float[,] flowGrid = new float[colCount, rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    flowGrid[i, j] = _cellGrid.Cells[i, j].WaterLevel;
                }
            }

            return flowGrid;
        }

        private void SetWaterLevels(float[,] flowGrid)
        {
            int colCount = _cellGrid.Cells.GetLength(0);
            int rowCount = _cellGrid.Cells.GetLength(1);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    _cellGrid.Cells[i, j].WaterLevel = flowGrid[i, j];
                }
            }
        }

        public void LoadCellGrid()
        {
            _cellGrid = new CellGrid(new Cell[5, 5]);

            for (int i = 0; i < _cellGrid.Cells.GetLength(1); i++)
            {
                for (int j = 0; j < _cellGrid.Cells.GetLength(0); j++)
                {
                    Cell cell;
                    if (i == 0 && j == 0)
                    {
                        cell = new Cell()
                        {
                            Capacity = 1000,
                            WaterLevel = 1000,
                            MaxFlowRate = 10,
                            Color = Brushes.Blue
                        };
                    }
                    else
                    {
                        cell = new Cell()
                        {
                            Capacity = 100,
                            WaterLevel = 0,
                            MaxFlowRate = 50,
                            Color = Brushes.Blue
                        };
                    }

                    _cellGrid.Cells[i, j] = cell;
                }
            }
        }

        public float CalculateTotalWater()
        {
            int colCount = _cellGrid.Cells.GetLength(0);
            int rowCount = _cellGrid.Cells.GetLength(1);
            float totalWater = 0;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    totalWater += _cellGrid.Cells[i, j].WaterLevel;
                }
            }

            Debug.WriteLine($"totalWater: {totalWater}");

            return totalWater;
        }
    }
}
