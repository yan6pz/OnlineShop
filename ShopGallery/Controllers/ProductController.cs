using BusinessLayer;
using DAL;
using ShopGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Product = ShopGallery.Models.Product;
using Subcategory = ShopGallery.Models.Subcategory;

namespace ShopGallery.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        private static int id;

        public ActionResult Show()
        {
            //TODO Need of some dependency resolver(structure map)
          var          databaseAccessInstance =new DatabaseAccess();      
          var          productModel   =new PopulateProduct();
          IProductable product = new ProductOrders(databaseAccessInstance, productModel);
          var products = product.Show();
            //TODO: Some data mapping.
            return View(products);
        }
        public ActionResult ShowProductById(int id)
        {

            Product product = new Product();
            List<Product> products = product.ShowProductById(id);
            return View(products);
            
        }
        public ActionResult Delete(int id)
        {

            var databaseAccessInstance = new DatabaseAccess();
            var productModel = new PopulateProduct();
            IProductable product = new ProductOrders(databaseAccessInstance, productModel);
             product.Delete(id);
            return RedirectToAction("Show", "Product");
           
        }

        public ActionResult ShowCategories()
        {

            List<Subcategory> subCat = new List<Subcategory>();
            Product product = new Product();
            return View(product.ShowCategories());

        }
        public ActionResult ShowSubcategories(int id)
        {

            List<Subcategory> subCat = new List<Subcategory>();
            Product product = new Product();
            return View(product.ShowSubcategories(id));

        }

        [HttpGet]
        public ActionResult Create()
        {
            var newProduct = new Product();
            
            return View("Create", newProduct);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {

            if (product.Image != null)
            {
                var databaseAccessInstance = new DatabaseAccess();
                var productModel = new PopulateProduct();
                IProductable productCreation = new ProductOrders(databaseAccessInstance, productModel);
                var result = productCreation.CreateProduct(product.Name,product.Color,product.UnitPrice
                                                            ,product.Size,product.Weight,product.IsDiscounted
                                                            ,product.SubcategoryID,product.Image);
                if (result)
                    RedirectToAction("Show","Product");
            }

            return RedirectToAction("Show");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            Product newProduct = new Product();

            return View("UpdateProduct", newProduct);
        }

        [HttpPost]
        public ActionResult Edit(Product p,int id)
        {

            var databaseAccessInstance = new DatabaseAccess();
            var productModel = new PopulateProduct();
            IProductable productCreation = new ProductOrders(databaseAccessInstance, productModel);
            var result = productCreation.UpdateProduct(id,p.Name, p.Color, p.UnitPrice
                                                        , p.Size, p.Weight);
            return RedirectToAction("Show");
        }

        public ActionResult CreateOrder(string ids, string qty)
        {
            var databaseAccessInstance = new DatabaseAccess();
            var productModel = new PopulateProduct();
            IProductable productCreation = new ProductOrders(databaseAccessInstance, productModel);

            var result = productCreation.CreateOrder(int.Parse(ids),decimal.Parse(qty), int.Parse(Session["UserId"].ToString()));
            return View();
        }

        [HttpGet]
        public ActionResult CreateOrder(int ProductID=0)
        {
            id = ProductID;
            return View("CreateOrder");
        }





    }
}
