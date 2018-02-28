using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostelService.Domain.Entities;

namespace HostelService.Domain.Abstract {
    public interface IRepository {
        IEnumerable<Customer> Customers { get; }
        void SaveCustomer(Customer customer);
        void DeleteCustomer(int customerId);

        IEnumerable<Booking> Bookings { get; }
        void SaveBooking(Booking booking);
        void DeleteBooking(int bookingId);
        //this function uses stored procedure
        IEnumerable<Booking> SearchBookingByCustomerId(int bookingid);

        IEnumerable<Card> Cards { get; }
        void SaveCard(Card card);
        void DeleteCard(int carId);
        
        IEnumerable<Account> Accounts { get; }
        void SaveAccount(Account account);
        void DeleteAccount(string email);
    }
}
