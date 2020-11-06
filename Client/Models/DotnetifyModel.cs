using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalRApp.Client.Models
{
    public class DotnetifyModel
    {
        public DotnetifyModel() { }

        public DotnetifyModel(int[] bar, double[] pie, string[][] waveform)
        {
            Bar = bar;
            Pie = pie;
            Waveform = waveform; 
        }
        public int[] Bar { get; set; }
        public double[] Pie { get; set; }
        public string[][] Waveform { get; set; }
    }
}
