using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace ShopGallery.Models
{
    public class Product
    {
        public static string connString = @"Data Source=.;Initial Catalog=OnlineShop;Integrated Security=True";
        public static string prodName = null;
        public static decimal prodPrice=0;
        List<Product> products = new List<Product>();
     

        public Product(int id,string name,string color,decimal unitPrice,
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


        
        //TODO:
       

        public List<Product> ShowProductById(int id)
        {

            using (SqlConnection connect = new SqlConnection(connString))
            {

                SqlCommand select = new SqlCommand("select * from Products where SubcategoryID=" + id, connect);
                select.Connection.Open();
                List<Product> products = new List<Product>();
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(int.Parse(reader["ProductID"].ToString()),
                                                reader["Name"].ToString(),
                                                reader["Color"].ToString(),
                                                decimal.Parse(reader["UnitPrice"].ToString()),
                                                reader["Size"].ToString(),
                                                int.Parse(reader["Weight"].ToString()),
                                                bool.Parse(reader["IsDiscounted"].ToString()),
                                                int.Parse(reader["SubcategoryID"].ToString()
                                                
                                                )));

                    }
                }

                return products;
            }
        }
        public List<Subcategory> ShowSubcategories(int id)
        {

            using (SqlConnection connect = new SqlConnection(connString))
            {

                SqlCommand select = new SqlCommand("select * from Subcategories where CategoryID=" + id, connect);
                select.Connection.Open();
                List<Subcategory> subcategories = new List<Subcategory>();
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subcategories.Add(new Subcategory(int.Parse(reader["SubcategoryID"].ToString()),
                                                        reader["SubcategoryName"].ToString()));


                    }
                }

                return subcategories;

            }
        }
        public List<Category> ShowCategories()
        {

            using (SqlConnection connect = new SqlConnection(connString))
            {

                SqlCommand select = new SqlCommand("select * from Categories", connect);
                select.Connection.Open();
                List<Category> categories = new List<Category>();
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category(int.Parse(reader["CategoryID"].ToString()),
                                                        reader["CategoryName"].ToString()));


                    }
                }

                return categories;
            }
        }

  

        public void CreateImage(byte[] image)
        {

            using (SqlConnection connect = new SqlConnection(connString))
            {

                SqlCommand insert = new SqlCommand("Insert into Products (Image)" +
                 "values (@image)", connect);


                insert.Parameters.Add(new SqlParameter("@image", image));


                insert.Connection.Open();
                insert.ExecuteNonQuery();

            }
        }
       
    }
}