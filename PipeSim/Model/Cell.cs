using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Text;

namespace PipeSim.Model
{
    public class Cell
    {
        public float Capacity { get; set; }
        public float WaterLevel { get; set; }
        public float MaxFlowRate { get; set; }
        public Brush Color { get; set; }
    }
}
