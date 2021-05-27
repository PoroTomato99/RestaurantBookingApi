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
    public class StatusController : ControllerBase
    {
        private readonly IStatus _status;
        private readonly ILogger<StatusController> _logger;

        public StatusController(IStatus status, ILogger<StatusController> logger)
        {
            _status = status;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetStatuses()
        {
            try
            {
                var statuses =  _status.GetStatuses();
                if (statuses == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No status found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No status found." });
                }
                return Ok(statuses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStatus(int? id)
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
                        Message = $"No id provided to query status information"
                    });
            }

            try
            {
                var status = _status.GetStatus(id);
                if (status != null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No status with id {id}  found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such status with id : {id}" });
                }
                return Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult PostStatus(Status x)
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
                var addStatus = _status.AddStatus(x);
                if (addStatus == null)
                {
                    _logger.LogWarning($"Error : {StatusCodes.Status500InternalServerError} Message : Some error occured while adding new status.");
                    return Conflict(new Response { Type = $"{StatusCodes.Status409Conflict}", Message = $"Conflict Adding New status" });
                }
                return Ok(addStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStatus(int? id)
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

            var status = _status.GetStatus(id);
            if (status == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no status found");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No status with Id : {id}"
                });
            }
            try
            {
                var x = _status.DeleteStatus(status);
                if (!x)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"Error Occured while attempting to delete status from database."
                    });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Deleted status with ID : {id}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutStatus(int? id, Status status)
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

            if (id != status.Id)
            {
                _logger.LogWarning($"{StatusCodes.Status400BadRequest}, Information might have been altered");
                return BadRequest(new Response 
                { 
                    Type = $"{StatusCodes.Status400BadRequest}",
                    Message = $"Information might have been altered." 
                });
            }

            var xStatus = _status.GetStatus(id);
            if (xStatus == null)
            {
                _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No status with ID : {id}.");
                return NotFound(new Response 
                {
                    Type = $"{StatusCodes.Status404NotFound}", 
                    Message = $"No status with ID : {id} found" 
                });
            }

            try
            {
                var x = _status.EditStatus(status);
                if (x == null)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"There are some errors occured while attempting to update status."
                    });
                }
                //return Ok(_tableData.GetTable(id));
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Update status with Id: {id}"
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