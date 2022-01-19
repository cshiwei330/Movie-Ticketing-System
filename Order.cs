using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Order
    {
        private int orderno;

        public int orderNo
        {
            get { return orderno; }
            set { orderno = value; }
        }
        private DateTime orderdateTime;

        public DateTime orderDateTime
        {
            get { return orderdateTime; }
            set { orderdateTime = value; }
        }
        private double amount;

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public List<Ticket> tList { get; set; } = new List<Ticket>();
        public Order() { }
        public Order(int orderNo, DateTime orderDateTime)
        {
            orderno = orderNo;
            orderdateTime = orderDateTime;
        }
        public void AddTicket(Ticket t)
        {
            tList.Add(t);
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
