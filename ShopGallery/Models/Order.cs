using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopGallery.Models
{
    public class Order
    {
        List<OrderDetail> details = new List<OrderDetail>();
        public static string connString = @"Data Source=.;Initial Catalog=OnlineShop;Integrated Security=True";
        public static int currUser = 0;
        public static bool isCurrUserAdmin = false;


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

        public Order(int id,decimal price, DateTime dateTime, int qty)
        {
            // TODO: Complete member initialization
            this.OrderID = id;
            this.Price = price;
            this.OrderDate = dateTime;
            this.Quantity = qty;
        }


        public int OrderID { get; set; }
        public int UserID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<decimal> Quantity { get; set; }


       

    }
}