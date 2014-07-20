using ShopGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopGallery.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult ShowUsers()
        {
            User user = new User();
            List<User> listU = user.ShowUsers();
         

            return View(listU);
        }
        [HttpGet]
        public ActionResult Create()
        {
            User newUser = new User();
            return View("Create",newUser);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            //
            if (ModelState.IsValid)
            {
               bool isUserExist= ShopGallery.Models.User.IsUserExist(user.Email);
               if (!isUserExist)
                {
                    user.CreateUser(user.UserName, user.FirstName, user.LastName, user.Email, user.Password, false);

                    return RedirectToAction("LogOn", "Home");
                }
                else
                {
                    return Content("There is existing user with that email"); 
                }
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public ActionResult Delete(int id)
        {
            User user = new User();
            user.Delete(id);
            return RedirectToAction("ShowUsers", "User");
        }

    }
}
