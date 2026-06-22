using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Text;

namespace PipeSim.Model
{
    class Wall : Cell
    {
        public Wall()
        {
            Capacity = 0;
            WaterLevel = 0;
            Color = Brushes.Gray;
        }
    }
}
