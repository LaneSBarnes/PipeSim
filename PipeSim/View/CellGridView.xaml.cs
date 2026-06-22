using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PipeSim.Model;
using PipeSim.ViewModel;

namespace PipeSim.View
{
    /// <summary>
    /// Interaction logic for CellGrid.xaml
    /// </summary>
    public partial class CellGridView : UserControl
    {
        CellGridViewModel _viewModel;

        public CellGridView()
        {
            InitializeComponent();

            _viewModel = new CellGridViewModel();
            DataContext = _viewModel;

            InitializeGrid();
        }

        public void InitializeGrid()
        {
            CellGrid cellGrid = _viewModel.CellGrid;

            for (int i = 0; i < cellGrid.Cells.GetLength(0); i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                BaseGrid.ColumnDefinitions.Add(column);
            }

            for (int j = 0; j < cellGrid.Cells.GetLength(1); j++)
            {
                RowDefinition row = new RowDefinition();
                BaseGrid.RowDefinitions.Add(row);
            }

            UpdateGrid();
        }

        public void UpdateGrid()
        {
            BaseGrid.Children.Clear();

            CellGrid cellGrid = _viewModel.CellGrid;

            for (int i = 0; i < cellGrid.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < cellGrid.Cells.GetLength(1); j++)
                {
                    Cell cell = cellGrid.Cells[i, j];
                    CellView cellView = new CellView(cell);

                    Brush brush = new SolidColorBrush(Colors.Blue);
                    brush.Opacity = (double)(cell.WaterLevel / cell.Capacity);
                    cellView.SetColor(brush);

                    Grid.SetRow(cellView, j);
                    Grid.SetColumn(cellView, i);
                    BaseGrid.Children.Add(cellView);
                }
            }
        }

        private void BaseGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.UpdateCellGrid();
            UpdateGrid();

            float totalWater = _viewModel.CalculateTotalWater();
            
        }
    }
}
