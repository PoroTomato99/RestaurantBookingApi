using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantBookingApi.Models;

namespace RestaurantBookingApi.RestaurantData
{
    public interface ITimeStatus
    {
        List<TimeStatus> GetTimeStatuses();
        TimeStatus GetTimeStatus(int? id);
        TimeStatus AddTimeStatus(TimeStatus status);
        bool DeleteTimeStatus(TimeStatus status);
        TimeStatus EditTimeStatus(TimeStatus status);
        bool IsTimeStatusExist(TimeStatus status);
    }
}
