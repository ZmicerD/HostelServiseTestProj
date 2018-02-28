using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HostelService.Domain.Abstract;
using HostelService.Domain.Entities;

namespace HostelService.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IRepository repository;

        public AdminController(IRepository repositoryParam) {
            this.repository = repositoryParam;
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account accountUser) {
            accountUser.Type = "customer";
            Account someUser = repository.Accounts.FirstOrDefault(u => u.Email == accountUser.Email);
            if (someUser == null) {
                if (ModelState.IsValid) {
                    repository.SaveAccount(accountUser);
                    Customer newCustomer = new Customer() {
                        Email = accountUser.Email
                    };
                    repository.SaveCustomer(newCustomer);
                    ModelState.Clear();
                    return RedirectToAction("Login", "Admin");
                } else {
                    ModelState.AddModelError("", "Model is not valid");
                }
            } else {
                ModelState.AddModelError("", "User with email " + accountUser.Email + " already exists");
            }
            return View();
        }
     
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account accountUser) {
            Account user = repository.Accounts.FirstOrDefault(u => u.Email == accountUser.Email & u.Password == accountUser.Password);
            if (user != null) {
                Session["UserID"] = repository.Customers.Single(u => u.Email == user.Email).CustomerID;
                Session["UserType"] = user.Type;
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                ModelState.AddModelError("", "Username or password is wrong");
            }
            return View();
        }

        public ActionResult Logout() {
            if (Session["UserID"] != null) {
                Session.Abandon();
            }
            return RedirectToAction("Login", "Admin");            
        }
    }
}