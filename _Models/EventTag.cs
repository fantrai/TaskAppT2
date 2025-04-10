using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2._Models
{
    public class EventTag(string name, string color)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string Color { get; set; } = color;
    }
}
