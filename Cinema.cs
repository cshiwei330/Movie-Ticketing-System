using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Cinema
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int hallNo;
        public int HallNo
        {
            get { return hallNo; }
            set { hallNo = value; }
        }
        private int capacity;
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
        public Cinema() { }
        public Cinema(string Name, int HallNo, int Capacity)
        {
            name = Name;
            hallNo = HallNo;
            capacity = Capacity;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
