using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface IAspRoleData
    {
        List<AspNetRole> GetRoles();
        AspNetUserRole GetRoleByCustId(string cusNo);
        //AspNetRole GetCustomerByUsername(string userName);
    }
}
