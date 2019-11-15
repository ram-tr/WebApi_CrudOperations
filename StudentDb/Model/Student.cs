using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDb.Model
{
    public class Student
    {
        public int RollNumber { set; get; }
        public string Name { set; get; }
        public string Branch { set; get; }
        public bool IsActive { set; get; }

    }
}
