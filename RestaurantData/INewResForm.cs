using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantBookingApi.Models;

namespace RestaurantBookingApi.RestaurantData
{
    public interface INewResForm
    {
        List<NewRestaurantForm> GetFroms();
        NewRestaurantForm GetForm(int? id);
        NewRestaurantForm AddForm(NewRestaurantForm table);
        bool DeleteForm(NewRestaurantForm table);
        NewRestaurantForm EditForm(NewRestaurantForm table);
        bool IsFormExist(NewRestaurantForm table);
    }
}
