using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Student
    {
        public string FullName { get; set; }
        public List<Record> Records { get; set; } = new List<Record>();

        public override string ToString() => FullName;
    }
}
