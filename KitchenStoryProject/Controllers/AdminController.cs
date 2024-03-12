using KitchenDALLib;
using KitchenStoryProject.Models;
using System;
using System.Web.Mvc;

namespace KitchenStoryProject.Controllers
{
    public class AdminController : Controller
    {
        AdminManager adminManagerDal = new AdminManager();
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminModel adminModel)
        {
            AdminMaster adminMaster = new AdminMaster()
            {
                EmailId = adminModel.EmailId,
                Password = adminModel.Password,
            };
            try
            {
                bool result = adminManagerDal.AdminLogin(adminMaster);
                if (result)
                {
                    return RedirectToAction("Index","FoodItem");
                }
                else
                {
                    return Content("Invalid Login Credentials");
                }
            }
            catch (Exception)
            {
                return Content("Invalid Login");
            }
        }

        public ActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordChange(AdminModel adminModel)
        {
            bool validEmail = adminManagerDal.validEmail(adminModel.EmailId);
            if (validEmail)
            {
                AdminMaster adminMaster = new AdminMaster()
                {
                    EmailId = adminModel.EmailId,
                    Password = adminModel.Password,
                };
                try
                {
                    bool result = adminManagerDal.ChangePassword(adminMaster);
                    if (result)
                    {
                        return RedirectToAction("SuccessPage");
                    }
                }
                catch (Exception)
                {
                    return Content("Invalid Login");
                }
            }
            return View();
        }

        public ActionResult SuccessPage()
        {
            return View();
        }
    }

}
