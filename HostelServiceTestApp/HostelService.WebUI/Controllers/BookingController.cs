using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HostelService.Domain.Abstract;
using HostelService.Domain.Entities;

namespace HostelService.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private IRepository repository;

        public BookingController(IRepository repositoryParam) {
            this.repository = repositoryParam;
        }

        public ActionResult BookingLookup(int? customerId) {
            if (Session["UserType"].ToString() == "admin" |
                Convert.ToInt32(Session["UserID"]) == customerId) {                
                return View(repository.Bookings
                   .Where(p => p.CustomerID == customerId));                
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        public ActionResult BookingLookupAll() {
            if (Session["UserID"] != null) {
                if (Session["UserType"].ToString() == "admin") {
                    return View("BookingLookup", repository.Bookings);
                } else {
                    return RedirectToAction("CustomerLookup", "Customer");
                }
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        //Uses SearchBookingByCustomerId stored procedure
        [HttpPost]
        public ActionResult BookingLookup(string customerIdParam) {
            if (Session["UserID"] != null) {
                if (Session["UserType"].ToString() == "admin") {
                    if (!String.IsNullOrEmpty(customerIdParam)) {
                        //return View("BookingLookup", repository.Bookings
                            //.Where(p => p.CustomerID == Convert.ToInt32(customerIdParam)));
                        return View("BookingLookup", repository.SearchBookingByCustomerId(Convert.ToInt32(customerIdParam)));
                    } else {
                        return View("BookingLookup", repository.Bookings);
                    }
                } else {
                    return RedirectToAction("CustomerLookup", "Customer");
                }
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

        public ActionResult BookingEdit(int? bookingIdParam) {
            if (Session["UserID"] != null) {
                if (Session["UserType"].ToString() == "admin") {
                    Booking bookingEdit = repository.Bookings
                        .FirstOrDefault(p => p.BookingID == bookingIdParam);
                    return View(bookingEdit);
                } else {
                    return RedirectToAction("CustomerLookup", "Customer");
                }
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }
        
        [HttpPost]
        public ActionResult BookingEdit(Booking bookingEdit) {
            if (Session["UserID"] != null) {
                if (Session["UserType"].ToString() == "admin") {
                    Customer customer = repository.Customers.FirstOrDefault(p => p.CustomerID == bookingEdit.CustomerID);
                    Card card = null;
                    if (bookingEdit.CardID != null) {
                        card = repository.Cards.FirstOrDefault(p => p.CardID == bookingEdit.CardID);
                    }
                    if (customer != null) {                        
                        if (ModelState.IsValid) {
                            if (card != null) {
                                if (card.CustomerID == customer.CustomerID) {
                                    repository.SaveBooking(bookingEdit);
                                    return RedirectToAction("CustomerLookup", "Customer");
                                } else {
                                    ModelState.AddModelError("", "Customer with ID=" + bookingEdit.CustomerID +
                                        " has not card with ID=" + bookingEdit.CardID);
                                }
                            } else {
                                if (bookingEdit.CardID == null) {
                                    repository.SaveBooking(bookingEdit);
                                    return RedirectToAction("CustomerLookup", "Customer");
                                } else {
                                    ModelState.AddModelError("", "Card with ID=" + bookingEdit.CardID + " does not exists");
                                }
                            }
                        } else {
                            ModelState.AddModelError("", "Model is not valid");
                        }                       
                    } else {
                        ModelState.AddModelError("", "Customer with id=" + bookingEdit.CustomerID +
                            " does not exists");
                    }                    
                }
                return View(bookingEdit);
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }        

        public ActionResult BookingCreate() {
            return View("BookingEdit", new Booking());
        }

        public ActionResult BookingDelete(string bookIdStr) {
            if (Session["UserType"].ToString() == "admin") {
                int bookId = Convert.ToInt32(bookIdStr); 
                repository.DeleteCard(bookId);
                return RedirectToAction("CustomerLookup", "Customer");
            } else if (Session["UserType"].ToString() == "customer") {
                return RedirectToAction("CustomerLookup", "Customer");
            } else {
                return RedirectToAction("Login", "Admin");
            }
        }

    }
}