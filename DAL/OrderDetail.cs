using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderDetail
    {
        public User CurrentUser { get; set; }
        public OrderDetail(int orderId, string prodName, decimal price, int qty)
        {

            this.OrderID = orderId;
            this.ProductName = prodName;
            this.Price = price;
            this.AvailableQTY = qty;
        }

        public OrderDetail()
        {
            // TODO: Complete member initialization
        }

        public static int OrderDetailsID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }
        public decimal AvailableQTY { get; set; }

        public string ProductName { get; set; }
    }
}
