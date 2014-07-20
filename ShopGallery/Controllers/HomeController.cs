using BusinessLayer;
using DAL;
using ShopGallery.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopGallery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        [HttpGet]
        public ViewResult LogOn()
        {
            
            return View("LogOn");
        }

        public ViewResult LogOff()
        {
            FormsAuthentication.SignOut();
            return View("LogOn");
        }

        public ViewResult Register()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult LogOn(Login model,bool rememberMe=true)
        {
            if (ModelState.IsValid)
            {
                if (model.IsUserExist(model.EmailId, model.Password))
                {
                    ViewBag.UserName = model.EmailId;
                    Session["Email"] = model.EmailId;

                    Session["IsAdmin"] = IsUserAdmin(model.EmailId);
                    FormsAuthentication.SetAuthCookie(model.EmailId,rememberMe);
                    FormsAuthentication.RedirectFromLoginPage(model.EmailId, false);
                }

                else
                {
                    ModelState.AddModelError("", "EmailId or Password Incorrect.");
                }
            }
            return View(model);
        }
        private bool IsUserAdmin(string email)
        {
            var databaseAccessInstance = new DatabaseAccess();
            var orderModel = new PopulateOrder();
            IOrderable product = new OrderActions(databaseAccessInstance, orderModel);
            var result = product.ShowCurrUser(email);
            Session["UserId"] = result.UserID;
            return result.IsAdmin;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                string realCaptcha = Session["captcha"].ToString();
                if (model.VerCode == realCaptcha)
                {
                    if (model.Insert())
                    {
                        return RedirectToAction("LogOn", "Home");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Email Id Already Exist");
                    }
                }
                else
                    ModelState.AddModelError("", "Verification Code is Incorrect");
            }
            return View(model);

        }

        //public  CaptchImageAction Image()
        //{

        //   string randomText = SelectRandomWord(6);

        //   Session["captcha"] = randomText;

        //   HttpContext.Session["RandomText"] = randomText;

        //   return new CaptchImageAction() { BackgroundColor = Color.LightGray, 
        //              RandomTextColor = Color.Black, RandomText = randomText };

        //}

    
    }   
}
