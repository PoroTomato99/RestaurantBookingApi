using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IAnnouncement
    {
        List<Annoucement> GetAnnoucements();
        Annoucement GetAnnoucement(int? id);
        Annoucement Post(Annoucement annoucement);
        void DeleteAnnouncement(Annoucement annoucement);
        Annoucement EditAnnouncement(Annoucement announcement);
    }
}
