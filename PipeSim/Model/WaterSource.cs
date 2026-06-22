using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Text;

namespace PipeSim.Model
{
    class WaterSource : Cell
    {
        public WaterSource()
        {
            Capacity = float.PositiveInfinity;
            WaterLevel = float.PositiveInfinity;
            Color = Brushes.Blue;
        }
    }
}
