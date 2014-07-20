using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Order
    {
        public static int currUser = 0;
        public static bool isCurrUserAdmin = false;
        List<OrderDetail> details = new List<OrderDetail>();

        public List<OrderDetail> Details
        {
            get
            {
                return
                    details;
                //sql metod with sql command
            }

        }

        public Order()
        {
            //this.OrderDetail = new HashSet<OrderDetail>();
        }

        public Order(int id, decimal price, DateTime dateTime, int qty)
        {
            // TODO: Complete member initialization
            this.OrderID = id;
            this.Price = price;
            this.OrderDate = dateTime;
            this.Quantity = qty;
        }


        public int OrderID { get; set; }
        public int UserID { get; set; }
        public decimal? Price { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Quantity { get; set; }
    }
}
