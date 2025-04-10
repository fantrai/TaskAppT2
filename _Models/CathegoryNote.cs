using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2._Models
{
    public class CathegoryNote(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string? Color { get; set; }
        public virtual List<Note> Notes { get; set; } = [];
    }
}
