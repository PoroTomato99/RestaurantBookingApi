using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IRestaurant
    {
        bool IsRestaurantExist(string restaurantName);
        List<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int? id);
        Restaurant AddRestaurant(NewRestaurantForm restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        Restaurant EditRestaurant(Restaurant restaurant);
    }
}
