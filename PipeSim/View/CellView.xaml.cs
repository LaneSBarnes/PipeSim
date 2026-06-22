using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PipeSim.Model;

namespace PipeSim.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CellView : UserControl
    {
        public CellView(Cell cell)
        {
            InitializeComponent();

            DataContext = cell;

            CellBorder.Background = cell.Color;
            CellText.Text = cell.WaterLevel.ToString("0.##");
        }

        public void SetColor(Brush color)
        {
            CellBorder.Background = color;
        }
    }
}
