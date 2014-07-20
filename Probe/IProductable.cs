using System.Web;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IProductable
    {
        bool CreateProduct(string productName,string color
                            ,decimal unitPrice,string size,int weight,bool discount
                            ,int subCatId,HttpPostedFileBase image);
  

        void CreateImage(byte[] image);

        bool UpdateProduct(int id, string prodName
                            , string color, decimal unitPrice,
                           string size, int weight);


        IEnumerable<Product> Show();


        bool Delete(int id);

        //TODO:
        bool CreateOrder(int id, decimal? qty,int userId);


        List<Product> ShowProductById(int id);

        List<Subcategory> ShowSubcategories(int id);

        List<Category> ShowCategories();

    }
}
