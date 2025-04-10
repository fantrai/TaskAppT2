using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2._Models
{
    public class CardSet(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string? Description { get; set; }
        public virtual List<Card> Cards { get; set; } = [];
        public virtual CardSetCathegory? CardSetCathegory { get; set; }

        [NotMapped]
        public bool CanCathegory { get { return CardSetCathegory != null; } }
    }
}
