using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostelService.Domain.Entities;
using HostelService.Domain.Abstract;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HostelService.Domain.Concrete {
    public class EFRepository : IRepository {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Customer> Customers {
            get { return new List<Customer>(context.Customer); }
        }

        public void SaveCustomer(Customer customer) {
            if (customer.CustomerID == 0) {
                context.Customer.Add(customer);
            } else {
                Customer dbEntry = context.Customer.Find(customer.CustomerID);
                if (dbEntry != null) {
                    dbEntry.SiteID = customer.SiteID;
                    dbEntry.LocalCustomerID = customer.LocalCustomerID;
                    dbEntry.Name = customer.Name;
                    dbEntry.Address1 = customer.Address1;
                    dbEntry.ZipCode = customer.ZipCode;
                    dbEntry.City = customer.City;
                    dbEntry.CountryID = customer.CountryID;
                    dbEntry.Email = customer.Email;
                    dbEntry.Phone = customer.Phone;
                    dbEntry.Mobile = customer.Mobile;
                    dbEntry.PermissionStatus = customer.PermissionStatus;
                    dbEntry.Gender = customer.Gender;
                    dbEntry.Category = customer.Category;
                }
            }
            context.SaveChanges();
        }

        public void DeleteCustomer(int customerId) {
            Customer dbEntry = context.Customer.Find(customerId);
            if (dbEntry != null) {
                context.Customer.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public IEnumerable<Booking> Bookings {
            get { return new List<Booking>(context.Booking); }
        }

        public void SaveBooking(Booking booking) {
            if (booking.BookingID == 0) {
                context.Booking.Add(booking);
            } else {
                Booking dbEntry = context.Booking.Find(booking.BookingID);
                if (dbEntry != null) {
                    dbEntry.CustomerID = booking.CustomerID;
                    dbEntry.CardID = booking.CardID;
                    dbEntry.BookingDate = booking.BookingDate;
                    dbEntry.FirstNightDate = booking.FirstNightDate;
                    dbEntry.NumberOfDays = booking.NumberOfDays;
                    dbEntry.NumberOfPersons = booking.NumberOfPersons;                   
                    dbEntry.LocalCustomerID = booking.LocalCustomerID;
                    dbEntry.SiteID = booking.SiteID;
                    dbEntry.CBBookingID = booking.CBBookingID;
                    dbEntry.Status = booking.Status;
                    dbEntry.Price = booking.Price;
                    dbEntry.TypeOfRoom = booking.TypeOfRoom;
                    dbEntry.SendToCsv = booking.SendToCsv;
                    dbEntry.Campaign = booking.Campaign;
                    dbEntry.Category = booking.Category;
                }
            }
            context.SaveChanges();
        }

        public void DeleteBooking(int bookingId) {
            Booking dbEntry = context.Booking.Find(bookingId);
            if (dbEntry != null) {
                context.Booking.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        //this function uses stored procedure
        public IEnumerable<Booking> SearchBookingByCustomerId(int customerId) {
            List<Booking> result = new List<Booking>();
            using (SqlConnection conn = new SqlConnection()) {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "pr_BookingLoadByCustomerID";
                sqlCommand.Connection = conn;
                sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", customerId));
                conn.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader()) {
                    while (reader.Read()) {
                        result.Add(new Booking() {
                            BookingID = Convert.ToInt32(reader["BookingID"]),
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CardID = Convert.IsDBNull(reader["CardID"]) ? 0 : Convert.ToInt32(reader["CardID"]),
                            BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                            FirstNightDate = Convert.ToDateTime(reader["FirstNightDate"]),
                            NumberOfDays = Convert.ToInt32(reader["NumberOfDays"]),
                            NumberOfPersons = Convert.ToInt32(reader["NumberOfPersons"]),
                            LocalCustomerID = Convert.ToInt32(reader["LocalCustomerID"]),
                            SiteID = Convert.ToString(reader["SiteID"]),
                            CBBookingID = Convert.IsDBNull(reader["CBBookingID"]) ? 0 : Convert.ToInt32(reader["CBBookingID"]),
                            Status =  Convert.ToString(reader["Status"]),
                            Price = Convert.IsDBNull(reader["Price"]) ? 0 : Convert.ToDecimal(reader["Price"]),
                            TypeOfRoom = Convert.ToString(reader["TypeOfRoom"]),
                            SendToCsv = Convert.ToBoolean(reader["LocalCustomerID"]),
                            Campaign = Convert.ToString(reader["Campaign"]),
                            Category = Convert.ToString(reader["Category"])
                        });
                    }
                }
                return result;
            }
        }

        public IEnumerable<Card> Cards {
            get { return new List<Card>(context.Card); }
        }

        public void SaveCard(Card card) {
            if (card.CardID == 0) {
                context.Card.Add(card);
            } else {
                Card dbEntry = context.Card.Find(card.CardID);
                if (dbEntry != null) {
                    dbEntry.SiteID = card.SiteID;
                    dbEntry.LocalCustomerID = card.LocalCustomerID;
                    dbEntry.CardNumber = card.CardNumber;
                    dbEntry.CustomerID = card.CustomerID;
                    dbEntry.IssueDate = card.IssueDate;
                    dbEntry.ExpirationDate = card.ExpirationDate;
                    dbEntry.BirthYear = card.BirthYear;
                    dbEntry.Email = card.Email;
                    dbEntry.Phone = card.Phone;
                    dbEntry.Mobile = card.Mobile;
                    dbEntry.PermissionStatus = card.PermissionStatus;                    
                }
            }
            context.SaveChanges();
        }

        public void DeleteCard(int cardId) {
            Card dbEntry = context.Card.Find(cardId);
            if (dbEntry != null) {
                context.Card.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public IEnumerable<Account> Accounts {
            get { return new List<Account>(context.Account); }
        }

        public void SaveAccount(Account account) {
            context.Account.Add(account);
            context.SaveChanges();
        }

        public void DeleteAccount(string email) {
            Account dbEntry = context.Account.Find(email);
            if (dbEntry != null) {
                context.Account.Remove(dbEntry);
                context.SaveChanges();
            }
        }
    }
}
