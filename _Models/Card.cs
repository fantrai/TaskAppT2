using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAppT2._Models
{
    public class Card(string term, string description)
    {
        public int Id { get; set; }
        public string Term { get; set; } = term;
        public string Description { get; set; } = description;
        public virtual List<CardSet>? CardSets { get; set; } = null!;
    }
}
