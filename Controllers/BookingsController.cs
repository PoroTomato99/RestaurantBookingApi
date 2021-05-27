using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantBookingApi.Models;
using RestaurantBookingApi.RestaurantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;

namespace RestaurantBookingApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBooking _booking;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IBooking booking, ILogger<BookingsController> logger)
        {
            _booking = booking;
            _logger = logger;
        }

        //Get : api/Bookings
        [HttpGet]
        public IActionResult GetBookings()
        {
            try
            {
                var bookings = _booking.GetBookings();
                if(bookings == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No bookings to be found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No Restaurant found." });
                }
                return Ok(bookings);
            }
            catch (Exception ex)
            { 
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"Some Internal Error Happened : {ex.Message}" });
            }
        }

        //Get : api/Bookings/{id}
        [HttpGet("{id}")]
        public IActionResult GetBooking(int? id)
        {
            if(id == null)
            {
                _logger.LogWarning($"" +
                    $"Type : {StatusCodes.Status409Conflict}," +
                    $" Error Message : No id provided.");

                return Conflict(
                    new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"No id provided to query booking information"
                    });
            }

            try
            {
                var booking = _booking.GetBooking(id);
                if(booking == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No booking with id {id} to be found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such booking with id : {id}" });
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        //Post: api/Bookings
        [HttpPost("{restaurantId}")]
        public IActionResult PostBooking(Booking booking, int? restaurantId)
        {
            if(restaurantId == null)
            {
                _logger.LogWarning($"" +
                                    $"Type : {StatusCodes.Status409Conflict}," +
                                    $" Error Message : No restaurant id provided.");

                return Conflict(
                    new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"No restaurant id provided"
                    });
            }

            if(booking == null)
            {
                _logger.LogWarning($"" +
                    $"Type : {StatusCodes.Status409Conflict}," +
                    $" Error Message : Null object value provided.");

                return Conflict(
                    new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"No value provided"
                    });
            }

            if (_booking.IsBookingExist(booking))
            {
                _logger.LogWarning($"" +
                    $"Type : {StatusCodes.Status409Conflict}," +
                    $" Error Message : Booking Already Existed.");

                return Conflict(
                    new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"One booking per date and per user."
                    });
            }
            try
            {
                var addBooking = _booking.AddBooking(booking, restaurantId);
                if(addBooking == null)
                {
                    _logger.LogWarning($"Error : {StatusCodes.Status500InternalServerError} Message : Some error occured while adding new booking.");
                    return Conflict(new Response { Type = $"{StatusCodes.Status409Conflict}", Message = $"Conflict Adding New Booking" });
                }
                //make publish to announcement queue
                RestaurantClassLibrary.Annoucement ann = new()
                {
                    Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                    Type = $"Booking No : {booking.Id} Status",
                    Message = $"We're sending this email to inform that your reservatation made on {booking.BookingDate} <br /> " +
                                 $"for {booking.ReservedTime} on {booking.ReservedDate} <br />" +
                                 $"is <strong>{Enum.GetName(typeof(ReservationStatus), booking.StatuId)}</strong>" +
                                 $"<br /> Please remember to provide this booking number :<p> {booking.Id} </p> to prove you reservation.",
                    UserId = booking.UserId
                };
                PublishAnnouncement(ann);
                return Ok(addBooking);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        //Delete : api/Bookings/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int? id)
        {
            if(id == null)
            {
                _logger.LogWarning($"" +
                    $"Type : {StatusCodes.Status409Conflict}," +
                    $" Error Message : No id provided.");

                return Conflict(new Response
                {
                    Type = $"{StatusCodes.Status409Conflict}",
                    Message = $"No Id provided."
                });
            }

            var booking = _booking.GetBooking(id);
            if(booking == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no restaurant found to be deleted");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No Product with Id : {id}"
                });
            }
            try
            {
                var x =_booking.DeleteBooking(booking);
                if (!x)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"Error Occured while attempting to delete booking from database."
                    });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Deleted Booking with ID : {id}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        enum ReservationStatus
        {
            Processing = 1,
            Booked = 2,
            Completed = 3,
            Cancelled = 4
        }
        //Put : api/Bookings/{id}
        [HttpPut("{id}")]
        public IActionResult PutBooking(int? id, Booking booking)
        {
            if(id == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, No booking id provided.");
                return Conflict(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No booking id provided." });
            }

            if(id != booking.Id)
            {
                _logger.LogWarning($"{StatusCodes.Status400BadRequest}, Information might have been altered");
                return BadRequest(new Response { Type = $"{StatusCodes.Status400BadRequest}", Message = $"Information might have been altered." });
            }

            var xBooking = _booking.GetBooking(id);
            if(xBooking == null)
            {
                _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No booking with ID : {id} to be found.");
                return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No booking with ID : {id} found" });
            }

            try
            {
               var x =  _booking.EditBooking(booking);
                if(x == null)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"There are some errors occured while attempting to update booking information."
                    });               
                }
                //return Ok(_tableData.GetTable(id));

                //make publish to announcement queue
                RestaurantClassLibrary.Annoucement ann = new()
                {
                    Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                    Type = $"Booking No : {booking.Id} Status",
                    Message = $"We're sending this email to inform that your reservatation made on {booking.BookingDate} <br /> " +
                                 $"for {booking.ReservedTime} on {booking.ReservedDate} <br />" +
                                 $"is <strong>{Enum.GetName(typeof(ReservationStatus), booking.StatuId)}</strong>" +
                                 $"<br /> Please remember to provide this booking number :<p> {booking.Id} </p> to prove you reservation.",
                    UserId = booking.UserId
                };
                PublishAnnouncement(ann);

                return Ok(new Response {
                    Type = $"{StatusCodes.Status200OK}", 
                    Message = $"Successfully Update booking information with Id: {id}" 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [NonAction]
        public void PublishAnnouncement(RestaurantClassLibrary.Annoucement annoucement)
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            try
            {
                annoucement.Date = DateTime.Now;
                bus.PubSub.Publish(new RestaurantClassLibrary.Annoucement
                {
                    Date = annoucement.Date,
                    Type = annoucement.Type,
                    Message = annoucement.Message,
                    UserId = annoucement.UserId

                });
                //return Ok(new Response { Type = "Success", Message = "Notification published to queue." });
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}.\n Error Message:Failed to publish notification to queue: {ex.Message}");
                //return BadRequest(new Response { Type = $"{StatusCodes.Status400BadRequest}", Message = $"Error Message: {ex.Message}" });
                //return false;
            }
        }
    }
}


