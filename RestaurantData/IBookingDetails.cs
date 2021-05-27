using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IBookingDetails
    {
        List<BookingDetail> GetBookingDetails(int? id);
        List<BookingDetail> GetUserBooking(string userId);
        BookingDetail GetBookingDetail(int? id);
        BookingDetail AddBookingDetail(BookingDetail bookingDetail);
        bool DeleteBookingDetail(BookingDetail bookingDetail);
        BookingDetail EditBookingDetail(BookingDetail bookingDetail);
        bool IsBookingDetailExist(BookingDetail bookingDetail);
    }
}
