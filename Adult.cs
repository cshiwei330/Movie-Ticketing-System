using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Adult:Ticket
    {
        private bool popcornOffer;

        public bool PopcornOffer
        {
            get { return popcornOffer; }
            set { popcornOffer = value; }
        }

    }
}
