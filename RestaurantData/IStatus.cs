using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantBookingApi.Models;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IStatus
    {
        List<Status> GetStatuses();
        Status GetStatus(int? id);
        Status AddStatus(Status status);
        bool DeleteStatus(Status status);
        Status EditStatus(Status status);
        bool IsStatusExist(Status status);
    }
}
