using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class Product
    {
        public Product(int id, string name, string color, decimal unitPrice,
           string size, int weight, bool discount, int subcatID, HttpPostedFileBase imageProd)
        {
            byte[] image;
            using (Stream inputStream = imageProd.InputStream)
            {
                var memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                image = memoryStream.ToArray();
            }
            this.ProductID = id;
            this.Name = name;
            this.Color = color;
            this.UnitPrice = unitPrice;
            this.Size = size;
            this.Weight = weight;
            this.IsDiscounted = discount;
            this.SubcategoryID = subcatID;
            this.ImageProduct = image;

        }
        public Product(int id, string name, string color, decimal unitPrice,
        string size, int weight, bool discount, int subcatID)
        {

            this.ProductID = id;
            this.Name = name;
            this.Color = color;
            this.UnitPrice = unitPrice;
            this.Size = size;
            this.Weight = weight;
            this.IsDiscounted = discount;
            this.SubcategoryID = subcatID;

        }


        public Product(string name)
        {
            this.Name = name;
        }
        public Product()
        {

        }



        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public string Size { get; set; }
        public int Weight { get; set; }
        public bool IsDiscounted { get; set; }
        public byte[] ImageProduct { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public int SubcategoryID { get; set; }
    }
}
