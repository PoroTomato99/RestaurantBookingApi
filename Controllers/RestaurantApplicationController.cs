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
    public class RestaurantApplicationController : ControllerBase
    {
        private readonly INewResForm _newResForm;
        private readonly ILogger<StatusController> _logger;

        public RestaurantApplicationController(INewResForm newResForm, ILogger<StatusController> logger)
        {
            _newResForm = newResForm;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetForms()
        {
            try
            {
                var applicationForms = _newResForm.GetFroms();
                if (applicationForms == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No restaurant applications found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No restaurant applications found." });
                }
                return Ok(applicationForms);
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
                        Message = $"No id provided to query information"
                    });
            }

            try
            {
                var x = _newResForm.GetForm(id);
                if (x != null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No restaurant application with id {id}  found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such restaurant application with id : {id}" });
                }
                return Ok(x);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult PostStatus(NewRestaurantForm x)
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
                var addStatus = _newResForm.AddForm(x);
                if (addStatus == null)
                {
                    _logger.LogWarning($"Error : {StatusCodes.Status500InternalServerError} Message : Some error occured while adding new restaurant registration.");
                    return Conflict(new Response { Type = $"{StatusCodes.Status409Conflict}", Message = $"Conflict Adding New restaurant registration" });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Sucessfully Submit Application!"
                });
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

            var existForm = _newResForm.GetForm(id);
            if (existForm == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no restaurant registration found");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No restaurant registration with Id : {id}"
                });
            }
            try
            {
                var x = _newResForm.DeleteForm(existForm);
                if (!x)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"Error Occured while attempting to delete from database."
                    });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Deleted restaurant registration with ID : {id}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutStatus(int? id, NewRestaurantForm newResForm)
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

            if (id != newResForm.Id)
            {
                _logger.LogWarning($"{StatusCodes.Status400BadRequest}, Information might have been altered");
                return BadRequest(new Response
                {
                    Type = $"{StatusCodes.Status400BadRequest}",
                    Message = $"Information might have been altered."
                });
            }

            var xForm = _newResForm.GetForm(id);
            if (xForm == null)
            {
                _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No registration with ID : {id}.");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No registration with ID : {id} found"
                });
            }

            try
            {
                var x = _newResForm.EditForm(newResForm);
                if (x == null)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"There are some errors occured while attempting to update."
                    });
                }
                //return Ok(_tableData.GetTable(id));
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Update registratin with Id: {id}"
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
