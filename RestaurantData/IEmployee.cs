using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IEmployee
    {
        List<AspNetUser> GetEmployees(int? restaurantId);
        AspNetUser GetEmployee(string employeeId);
        AspNetUser GetEmployeeByUsername(string userName);
        bool DeleteEmployee(AspNetUser customer);
    }
}
