using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HostelService.Domain.Abstract;
using HostelService.Domain.Entities;

namespace HostelService.WebUI.Controllers {
    public class CardController : Controller {
        private IRepository repository;

        public CardController(IRepository repositoryParam) {
            this.repository = repositoryParam;
        }

        public ActionResult CardLookup(int? customerId) {
            if (Session["UserID"] != null) {
                if (Convert.ToInt32(Session["UserID"]) == customerId
                    | Session["UserType"].ToString() == "admin") {
                    Card someCard = repository.Cards
                        .FirstOrDefault(p => p.CustomerID == customerId);
                    if (someCard != null) {                        
                        return View(repository.Cards
                            .Where(p => p.CustomerID == customerId));
                    } else {
                        return View(new List<Card>());
                    }
                } else {
                    return RedirectToAction("CustomerLookup", "Customer");
                }
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        public ActionResult CardLookupAll() {
            if (Session["UserID"] != null) {
                if (Session["UserType"].ToString() == "admin") {
                    return View("CardLookup", repository.Cards);
                } else {
                    return RedirectToAction("CustomerLookup", "Customer");
                }
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        public ActionResult CardLookup(string customerIdParam) {
            if (Session["UserType"].ToString() == "admin") {                
                if (!String.IsNullOrEmpty(customerIdParam)) {
                    return View("CardLookup", repository.Cards
                        .Where(p => p.CustomerID == Convert.ToInt32(customerIdParam)));
                } else {
                    return View("CardLookup", repository.Cards);
                }                
            } else if (Session["UserType"].ToString() == "customer") {
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        public ActionResult CardEdit(int? cardId) {
            if (Session["UserType"].ToString() == "admin") {
                Card cardEdit = repository.Cards
                    .FirstOrDefault(p => p.CardID == cardId);
                return View(cardEdit);
            } else if (Session["UserType"].ToString() == "customer") {
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        public ActionResult CardEdit(Card card) {
            if (Session["UserType"].ToString() == "admin") {
                Customer customer = repository.Customers
                    .FirstOrDefault(p => p.CustomerID == card.CustomerID);
                if (customer != null) {
                    if (ModelState.IsValid) {                        
                        repository.SaveCard(card);
                        return RedirectToAction("CustomerLookup", "Customer");
                    } else {
                        ModelState.AddModelError("", "Model is not valid");
                    }
                } else {
                    ModelState.AddModelError("", "Customer with id=" + card.CustomerID + " does not exists");
                }
                return View(card);                    
            } else if (Session["UserType"].ToString() == "customer") {
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        public ActionResult CardCreate() {
            if (Session["UserType"].ToString() == "admin") {
                return View("CardEdit", new Card());
            } else if (Session["UserType"].ToString() == "customer") {
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }
        
        public ActionResult CardDelete(string cardIdStr) {
            if (Session["UserType"].ToString() == "admin") {
                int cardId = Convert.ToInt32(cardIdStr);
                IEnumerable<Booking> bookings =
                    repository.Bookings.Where(p => p.CardID == cardId);
                foreach (Booking book in bookings) {
                    Booking forEd = new Booking();
                    forEd = book;
                    forEd.CardID = null;
                    repository.SaveBooking(forEd);
                }
                repository.DeleteCard(cardId);
                return RedirectToAction("CustomerLookup", "Customer");
            } else if (Session["UserType"].ToString() == "customer") {
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }
    }
}