using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface ICustomerData
    {
        List<AspNetUser> GetCustomers();
        AspNetUser GetCustomer(string cusNo);

        AspNetUser GetCustomerByUsername(string userName);

        Task<RegisterModel> AddCustomer(RegisterModel customer);
        void DeleteCustomer(AspNetUser customer);
        AspNetUser EditCustomer(AspNetUser customer);
    }
}
