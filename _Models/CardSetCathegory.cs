using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskAppT2._Models
{
    public class CardSetCathegory(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public virtual List<CardSet>? CardSets { get; set; }
    }
}
