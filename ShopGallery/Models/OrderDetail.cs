using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopGallery.Models
{
    public class OrderDetail
    {


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
        public int AvailableQTY { get; set; }

        public string ProductName { get; set; }

       
    }
}