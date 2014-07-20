using System.Web;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Category = DAL.Category;
using Product = DAL.Product;
using Subcategory = DAL.Subcategory;

namespace BusinessLayer
{
    public class ProductOrders:IProductable
    {

        public IDatabaseAccess          DatabaseAccessInstance           { get; private set; }
        public PopulateProduct             ProductModel                        { get; private set; }

        public ProductOrders(DatabaseAccess accessdata
                                                                           , PopulateProduct userModel)
       {
           this.DatabaseAccessInstance                              = accessdata;
           this.ProductModel = userModel;
       }

        public bool CreateProduct(string productName, string color
                            , decimal unitPrice, string size, int weight, bool discount,int subcatId,HttpPostedFileBase image)
        {
            var product         = new Product(0, productName, color, unitPrice, size, weight, discount, subcatId, image);
            var result          = DatabaseAccessInstance.Create(SqlCommands.CREATE_PRODUCT,product);
            return result;
        }

        public void CreateImage(byte[] image)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(int id, string prodName, string color, decimal unitPrice, string size, int weight)
        {
            var result = DatabaseAccessInstance.UpdateProduct( id,  prodName,  color,  unitPrice,  size,  weight);
            return result;
        }

        public IEnumerable<Product> Show()
        {
            var result = DatabaseAccessInstance.ExecuteReader(SqlCommands.SHOW_PRODUCTS, ProductModel.ShowAll);
            return result;
        }

        public bool Delete(int id)
        {
            var result = DatabaseAccessInstance.Delete(SqlCommands.DELETE_PRODUCT,id);
            return result;
        }

        public bool CreateOrder(int id, decimal? qty,int userId)
        {
            var productData = DatabaseAccessInstance.ExecuteReader(SqlCommands.GETPRODUCT_ORDER, id.ToString(), ProductModel.PopulatePartial);
            
            var order = DatabaseAccessInstance.CreateOrder(SqlCommands.CREATE_ORDER, productData.FirstOrDefault(), userId,qty);
           
            return order;
        }

        public List<Product> ShowProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Subcategory> ShowSubcategories(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> ShowCategories()
        {
            throw new NotImplementedException();
        }
    }
}
