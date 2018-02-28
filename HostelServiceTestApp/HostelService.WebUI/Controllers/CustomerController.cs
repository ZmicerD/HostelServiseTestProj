using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HostelService.Domain.Abstract;
using HostelService.Domain.Entities;

namespace HostelService.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private IRepository repository;

        public CustomerController(IRepository repositoryPram) {
            this.repository = repositoryPram;
        }

        public ActionResult CustomerLookup() {
            if (Session["UserID"] != null) {
                if (Session["UserType"].ToString() == "customer") {
                    return View(repository.Customers
                        .Where(p => p.CustomerID == Convert.ToInt32(Session["UserID"])));
                } else {
                    return View(repository.Customers);
                }
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        public ActionResult CustomerLookup(string customerIdParam) {
            if (Session["UserType"].ToString() == "admin") {
                if (!String.IsNullOrEmpty(customerIdParam)) {
                    return View(repository.Customers
                        .Where(p => p.CustomerID == Convert.ToInt32(customerIdParam))
                        .OrderBy(p => p.CustomerID));
                } else {
                    return View(repository.Customers);
                }
            } else if (Session["UserType"].ToString() == "customer") {
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        public ActionResult CustomerEdit(int? id) {
            if (Session["UserID"] != null) {
                Customer customerEdit = repository.Customers
                    .FirstOrDefault(p => p.CustomerID == id);
                return View(customerEdit);
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }
        
        [HttpPost]
        public ActionResult CustomerEdit(Customer customer) {
            if (Session["UserID"] != null) {
                if (ModelState.IsValid) {
                    if (repository.Customers.FirstOrDefault(p => p.Email == customer.Email) == null) {
                        Customer customerAccount = repository.Customers.FirstOrDefault(p => p.CustomerID == customer.CustomerID);
                        Account account = repository.Accounts.FirstOrDefault(p => p.Email == customerAccount.Email);
                        account.Email = customer.Email;
                        repository.SaveAccount(account);
                        repository.SaveCustomer(customer);
                        return RedirectToAction("CustomerLookup", "Customer");
                    } else {
                        ModelState.AddModelError("", "Customer with email=" + customer.Email + " already exists");
                    }
                } else {
                    ModelState.AddModelError("", "Model is not valid");
                }
                return View(customer);
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }        

        public ActionResult CustomerDelete(string customerIdStr) {
            if (Session["UserType"].ToString() == "admin") {
                int customerId = Convert.ToInt32(customerIdStr);
                IEnumerable<Booking> bookings = repository.Bookings
                    .Where(p => p.CustomerID == customerId);
                foreach (Booking book in bookings) {
                    repository.DeleteBooking(book.BookingID);
                }
                IEnumerable<Card> cards = repository.Cards
                    .Where(p => p.CustomerID == customerId);
                foreach (Card card in cards) {
                    repository.DeleteCard(card.CardID);
                }
                Customer customer = repository.Customers.FirstOrDefault(p => p.CustomerID == customerId);
                repository.DeleteAccount(customer.Email);
                repository.DeleteCustomer(customerId);
                return RedirectToAction("CustomerLookup", "Customer");
            } else if (Session["UserType"].ToString() == "customer") {
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

    }
}