using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2._Models
{
    public class Teacher(string name, string surname)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
        public string? Patronymic { get; set; }
        public virtual List<string> Phones { get; set; } = [];
        public virtual List<string> Links { get; set; } = [];
        public string? WorkTime { get; set; }
        public virtual List<Subject> Subjects { get; set; } = [];

        [NotMapped]
        public string ShortName 
        { 
            get 
            {
                string res = Surname + " " + Name[0] + ".";
                if (Surname != null)
                {
                    res += Surname[0] + " .";
                }
                return res;
            } 
        }
    }
}
