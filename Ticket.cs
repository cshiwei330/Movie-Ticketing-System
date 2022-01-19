using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Ticket:Screening
    {
        private Screening screening;

        public Screening Screening
        {
            get { return screening; }
            set { screening = value; }
        }
        public Ticket() : base() { }
        public Ticket(Screening s)
        {
            Screening = s;
        }
        public virtual double CalculatePrice()
        {
            return 1.0;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
