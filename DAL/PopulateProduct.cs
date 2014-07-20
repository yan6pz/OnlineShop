using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PopulateProduct : IPopulateProduct
    {
        public Product ShowAll(IDataReader arg)
        {
            var product = new Product();
            byte[] imageArray;
            try
            {
                imageArray = File.ReadAllBytes(@"D:\C#\projects\ShopGallery\ShopGallery\ShopGallery\Images\no-image.jpg");
             
            }
            catch (Exception)
            {
                imageArray=new byte[0];
            }
                product.ProductID = int.Parse(arg["ProductID"].ToString());
                product.Name = arg["Name"].ToString();
                product.Color = arg["Color"].ToString();
                product.UnitPrice = decimal.Parse(arg["UnitPrice"].ToString());
                product.Size = arg["Size"].ToString();
                product.IsDiscounted = bool.Parse(arg["IsDiscounted"].ToString());
                product.SubcategoryID = int.Parse(arg["SubcategoryID"].ToString());
                product.ImageProduct = (arg["Image"] as byte[]) != null ? arg["Image"] as byte[] : imageArray;
                product.Weight = int.Parse(arg["Weight"].ToString());

                return product;
                    

            
        }

        public Product PopulatePartial(IDataReader arg)
        {
           
            var product = new Product();
            product.ProductID = int.Parse(arg["ProductID"].ToString());
            product.Name = arg["Name"].ToString();
            product.UnitPrice = decimal.Parse(arg["UnitPrice"].ToString());
            return product;
        }
    }
}
