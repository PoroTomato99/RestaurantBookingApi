using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Valid Email Address")]
        public string Email { get; set; }
    }
}
