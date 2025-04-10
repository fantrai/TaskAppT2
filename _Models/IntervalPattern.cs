using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2._Models
{
    public class IntervalPattern(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string? Description { get; set; }
        public virtual List<Interval> Intervals { get; set; } = [];
    }
}
