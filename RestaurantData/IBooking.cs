using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IBooking
    {
        List<Booking> GetBookings();
        Booking GetBooking(int? id);
        Booking AddBooking(Booking booking, int? restaurantId);
        bool DeleteBooking(Booking booking);
        Booking EditBooking(Booking booking);
        bool IsBookingExist(Booking booking);
    }
}
