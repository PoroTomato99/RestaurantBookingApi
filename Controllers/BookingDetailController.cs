using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantBookingApi.Models;
using RestaurantBookingApi.RestaurantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDetailController : ControllerBase
    {
        private readonly IBookingDetails _bookingDetail;
        private readonly ILogger<BookingDetailController> _logger;

        public BookingDetailController(IBookingDetails bookingDetails, ILogger<BookingDetailController> logger)
        {
            _bookingDetail = bookingDetails;
            _logger = logger;
        }


        //Get : api/Bookings
        [HttpGet("restaurantId/{id}")]
        public IActionResult GetBookingDetails(int? id)
        {
            try
            {
                var bookingDetails = _bookingDetail.GetBookingDetails(id);
                if (bookingDetails == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No booking details found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No booking details found." });
                }
                return Ok(bookingDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpGet("userId/{id}")]
        public IActionResult GetUserBooking(string id)
        {
            if (id == null)
            {
                _logger.LogWarning($"" +
                    $"Type : {StatusCodes.Status409Conflict}," +
                    $" Error Message : No user id provided.");

                return Conflict(
                    new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"No user id provided to query booking details information"
                    });
            }
            try
            {
                var bookList = _bookingDetail.GetUserBooking(id);
                if (bookList == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No booking details with user ID: {id} to be found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such booking details with ID : {id}" });
                }
                return Ok(bookList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingDetail(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning($"" +
                    $"Type : {StatusCodes.Status409Conflict}," +
                    $" Error Message : No id provided.");

                return Conflict(
                    new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"No id provided to query booking details information"
                    });
            }

            try
            {
                var bookingDetail = _bookingDetail.GetBookingDetail(id);
                if (bookingDetail == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No booking details with id {id} to be found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such booking details with id : {id}" });
                }
                return Ok(bookingDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult PostBookingDetail(BookingDetail bookingDetail)
        {
            if (bookingDetail == null)
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

            try
            {
                var addBooking = _bookingDetail.AddBookingDetail(bookingDetail);
                if (addBooking == null)
                {
                    _logger.LogWarning($"Error : {StatusCodes.Status500InternalServerError} Message : Some error occured while adding new booking detail.");
                    return Conflict(new Response { Type = $"{StatusCodes.Status409Conflict}", Message = $"Conflict Adding New booking detail" });
                }
                return Ok(addBooking);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBookingDetail(int? id)
        {
            if (id == null)
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

            var bookingDetail = _bookingDetail.GetBookingDetail(id);
            if (bookingDetail == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no booking detail found");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No booking details with Id : {id}"
                });
            }
            try
            {
                var x = _bookingDetail.DeleteBookingDetail(bookingDetail);
                if (!x)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"Error Occured while attempting to delete booking details from database."
                    });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Deleted Booking Detail with ID : {id}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutBookingDetail(int? id, BookingDetail bookingDetail)
        {
            if (id == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, No id provided.");
                return Conflict(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No id provided." });
            }

            if (id != bookingDetail.Id)
            {
                _logger.LogWarning($"{StatusCodes.Status400BadRequest}, Information might have been altered");
                return BadRequest(new Response { Type = $"{StatusCodes.Status400BadRequest}", Message = $"Information might have been altered." });
            }

            var xBooking = _bookingDetail.GetBookingDetail(id);
            if (xBooking == null)
            {
                _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No booking detail with ID : {id}.");
                return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No booking detail with ID : {id} found" });
            }

            try
            {
                var x = _bookingDetail.EditBookingDetail(bookingDetail);
                if (x == null)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"There are some errors occured while attempting to update booking detail information."
                    });
                }
                //return Ok(_tableData.GetTable(id));
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Update booking detail information with Id: {id}"
                });
            }
            catch (Exception ex)
            {
                {
                    _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                    return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
                }
            }
        }
    }
}
