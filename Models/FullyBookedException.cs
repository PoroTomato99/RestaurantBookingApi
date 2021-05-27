using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.Models
{
    [Serializable]
    public class FullyBookedException : Exception
    {
        public FullyBookedException()
        {

        }

        public FullyBookedException(string time, string date) : base(String.Format("Time is Fully Book: {0} on {1}", time, date))
        {

        }
    }
}
