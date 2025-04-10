using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2._Models
{
    public class Interval(IntervalType type)
    {
        public int Id { get; set; }
        public int Minutes { get; set; } = 10;
        public int Seconds { get; set; }
        public IntervalType Type { get; set; } = type;
    }

    public enum IntervalType 
    { 
        Relax,
        Job
    }
}
