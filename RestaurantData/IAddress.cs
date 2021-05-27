using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantBookingApi.Models;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IAddress
    {
        List<Address> GetAddresses();
        Address GetAddress(int? restaurantId);
        Address AddAddress(Address address);
        void DeleteAddress(Address address);
        Address EditAddress(Address address);
    }
}
