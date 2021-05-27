using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.Models
{
    public class LockUserModel
    {
        public string UserId { get; set; }
        public string LockoutEnd { get; set; }
    }
}
