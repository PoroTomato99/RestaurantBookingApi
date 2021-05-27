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
    public class RestaurantUserController : ControllerBase
    {
        private readonly IEmployee _employee;
        private readonly ILogger<RestaurantUserController> _logger;

        public RestaurantUserController(IEmployee employee, ILogger<RestaurantUserController> logger)
        {
            _employee = employee;
            _logger = logger;
        }

        [HttpGet("restaurantId/{restaurantId}")]
        public IActionResult GetEmployees(int? restaurantId)
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
                        Message = $"No id provided."
                    });
            }
            try
            {
                var employeeList = _employee.GetEmployees(restaurantId);
                if (employeeList == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No Employees to be found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No Employees found." });
                }
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"Some Internal Error Happened : {ex.Message}" });
            }
        }

        [HttpGet("userId/{employeeId}")]
        public IActionResult GetById(string employeeId)
        {
            try
            {
                var employee = _employee.GetEmployee(employeeId);
                return employee != null ? Ok(employee) : (IActionResult)NotFound("No Employee Found!");
                //if (employee != null)
                //{
                //    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No employee with ID :{employeeId} to be found.");
                //    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such employee with ID : {employeeId}" });
                //}
                //return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteProfile(string userId)
        {
            if (userId == null)
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

            var employee = _employee.GetEmployee(userId);
            if (employee == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no employee found");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No employee with Id : {userId}"
                });
            }
            try
            {
                var x = _employee.DeleteEmployee(employee);
                if (!x)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"Error Occured while attempting to delete employee from database."
                    });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Deleted Booking with ID : {userId}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpGet("{userName}")]
        public IActionResult GetEmployeeByUsername(string userName)
        {
            //if (userName == null)
            //{
            //    _logger.LogWarning($"{StatusCodes.Status404NotFound}, No username provided.");
            //    return Conflict(new Response
            //    {
            //        Type = $"{StatusCodes.Status404NotFound}",
            //        Message = $"No username provided."
            //    });
            //}
            try
            {
                var employee = _employee.GetEmployeeByUsername(userName);
                //if (employee != null)
                //{
                //    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No employee with User Name :{userName} to be found.");
                //    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such employee with User Name : {userName}" });
                //}
                //return Ok(employee);
                if(employee == null)
                {
                    return NotFound(new Response
                    {
                        Type = $"{StatusCodes.Status404NotFound}",
                        Message = $"No Employee found with usernamae : {employee.UserName}"
                    });
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }
    }
}
