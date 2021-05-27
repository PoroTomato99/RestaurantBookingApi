using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.Models
{
    public class ConfirmEmailModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
