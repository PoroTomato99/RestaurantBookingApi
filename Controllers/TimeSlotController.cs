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
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlot _timeSlot;
        private readonly ILogger<TimeSlotController> _logger;

        public TimeSlotController(ITimeSlot timeSlot, ILogger<TimeSlotController> logger)
        {
            _timeSlot = timeSlot;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetTimeSlots()
        {
            try
            {
                var timeSlot = _timeSlot.GetAllTimeSlot();
                if (timeSlot == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No time slots found.");
                    return NotFound(new Response
                    {
                        Type = $"{StatusCodes.Status404NotFound}",
                        Message = $"No time slots found."
                    });
                }
                return Ok(timeSlot);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpGet("restaurantTime/{restaurantId}")]
        public IActionResult GetRestaurantTimeSlots(int? restaurantId)
        {
            if (restaurantId == null)
            {
                _logger.LogWarning($"" +
                    $"Type : {StatusCodes.Status409Conflict}," +
                    $" Error Message : No id provided.");

                return Conflict(
                    new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"No id provided to query time slot information"
                    });
            }

            try
            {
                var timeSlot = _timeSlot.GetTimeSlots(restaurantId);
                if (timeSlot == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No time slots found.");
                    return NotFound(new Response
                    {
                        Type = $"{StatusCodes.Status404NotFound}",
                        Message = $"No time slots found."
                    });
                }
                return Ok(timeSlot);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTimeSlot(int? id)
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
                        Message = $"No id provided to query time slot information"
                    });
            }

            try
            {
                var status = _timeSlot.GetTimeSlot(id);
                if (status != null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No time slot with id {id} found.");
                    return NotFound(new Response
                    {
                        Type = $"{StatusCodes.Status404NotFound}",
                        Message = $"No such time slot with id : {id}"
                    });
                }
                return Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response 
                { 
                    Type = $"{ex.GetType()}", 
                    Message = $"{ex.Message}" 
                });
            }
        }

        [HttpPost]
        public IActionResult PostTimeSlot(TimeSlot x)
        {
            if (x == null)
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
                var addTimeSlot = _timeSlot.AddTimeSlot(x);
                if (addTimeSlot == null)
                {
                    _logger.LogWarning($"Error : {StatusCodes.Status500InternalServerError} Message : Some error occured while adding new time slot.");
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"Conflict Adding New time slot"
                    });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successully add new time slot to restaurant"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTimeSlot(int? id)
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

            var timeSlot = _timeSlot.GetTimeSlot(id);
            if (timeSlot == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no time slot found");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No time slot with Id : {id}"
                });
            }
            try
            {
                var x = _timeSlot.DeleteTimeSlot(timeSlot);
                if (!x)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"Error Occured while attempting to delete time slot from database."
                    });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Deleted time slot with ID : {id}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutTimeSlot(int? id, TimeSlot timeSlot)
        {
            if (id == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, No id provided.");
                return Conflict(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No id provided."
                });
            }

            if (id != timeSlot.Id)
            {
                _logger.LogWarning($"{StatusCodes.Status400BadRequest}, Information might have been altered");
                return BadRequest(new Response
                {
                    Type = $"{StatusCodes.Status400BadRequest}",
                    Message = $"Information might have been altered."
                });
            }

            var xTimeSlot = _timeSlot.GetTimeSlot(id);
            if (xTimeSlot == null)
            {
                _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No time slot with ID : {id}.");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No time slot with ID : {id} found"
                });
            }

            try
            {
                var x = _timeSlot.EditTimeSlot(timeSlot);
                if (x == null)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"There are some errors occured while attempting to update time slot."
                    });
                }
                //return Ok(_tableData.GetTable(id));
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Update time slot with Id: {id}"
                });
            }
            catch (Exception ex)
            {
                {
                    _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                    return Conflict(new Response
                    {
                        Type = $"{ex.GetType()}",
                        Message = $"{ex.Message}"
                    });
                }
            }
        }
    }
}
