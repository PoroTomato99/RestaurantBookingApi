using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantBookingApi.Models;

namespace RestaurantBookingApi.RestaurantData
{
    public interface ITimeSlot
    {
        List<TimeSlot> GetAllTimeSlot();
        List<TimeSlot> GetTimeSlots(int? restaurantId);
        TimeSlot GetTimeSlot(int? id);
        TimeSlot AddTimeSlot(TimeSlot timeSlot);
        bool DeleteTimeSlot(TimeSlot timeSlot);
        TimeSlot EditTimeSlot(TimeSlot timeSlot);
        bool IsTimeSlotExist(TimeSlot timeSlot);
    }
}