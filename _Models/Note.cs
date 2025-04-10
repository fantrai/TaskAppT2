using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2._Models
{
    public class Note(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string? Text { get; set; }
        public string? Color { get; set; }
        public virtual List<CathegoryNote> Cathegories { get; set; } = [];
    }
}
