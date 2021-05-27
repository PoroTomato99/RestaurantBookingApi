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
    public class EmployeeProfileController : ControllerBase
    {
        private readonly IUserProfile _profile;
        private readonly ILogger<EmployeeProfileController> _logger;

        public EmployeeProfileController(IUserProfile profile, ILogger<EmployeeProfileController> logger)
        {
            _profile = profile;
            _logger = logger;
        }

        //Get : api/Bookings
        [HttpGet("restaurantId/{restaurantId}")]
        public IActionResult GetProfiles(int? restaurantId)
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
                var employeeList = _profile.GetUserProfiles(restaurantId);
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

        //Get : api/Bookings/{id}
        [HttpGet("userId/{userId}")]
        public IActionResult GetProfile(string userId)
        {
            if (userId == null)
            {
                _logger.LogWarning($"" +
                    $"Type : {StatusCodes.Status409Conflict}," +
                    $" Error Message : No user id provided.");

                return Conflict(
                    new Response
                    {
                        Type = $"{StatusCodes.Status409Conflict}",
                        Message = $"No user id provided."
                    });
            }

            try
            {
                var employee = _profile.GetUserProfile(userId);
                if (employee == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No employee with ID :{userId} to be found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such employee with ID : {userId}" });
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        //Post: api/Bookings
        [HttpPost]
        public IActionResult PostProfile(UserProfile profile)
        {
            if (profile == null)
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
                var addProfile = _profile.AddUserProfile(profile);
                if (addProfile == null)
                {
                    _logger.LogWarning($"Error : {StatusCodes.Status500InternalServerError} Message : Some error occured while adding new profile.");
                    return Conflict(new Response { Type = $"{StatusCodes.Status409Conflict}", Message = $"Conflict Adding New profile" });
                }
                //return Ok(addProfile);
                return Ok(new Response
                {
                    Type =$"{StatusCodes.Status200OK}",
                    Message = $"Sucessfully Added Profile for {profile.FirstName} {profile.LastName}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        //Delete : api/Bookings/{id}
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

            var profile = _profile.GetUserProfile(userId);
            if (profile == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no profile found");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No profile with Id : {userId}"
                });
            }
            try
            {
                var x = _profile.DeleteUserProfile(profile);
                if (!x)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"Error Occured while attempting to delete profile from database."
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

        //Put : api/Bookings/{id}
        [HttpPut("{userId}")]
        public IActionResult PutFrofile(string userId, UserProfile profile)
        {
            if (userId == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, No profile id provided.");
                return Conflict(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No profile id provided." });
            }

            if (userId != profile.Id)
            {
                _logger.LogWarning($"{StatusCodes.Status400BadRequest}, Information might have been altered");
                return BadRequest(new Response { Type = $"{StatusCodes.Status400BadRequest}", Message = $"Information might have been altered." });
            }

            var xProfile = _profile.GetUserProfile(userId);
            if (xProfile == null)
            {
                _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No profile with ID : {userId} to be found.");
                return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No booking with ID : {userId} found" });
            }

            try
            {
                var x = _profile.EditUserProfile(profile);
                if (x == null)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"There are some errors occured while attempting to update profile information."
                    });
                }
                //return Ok(_tableData.GetTable(id));
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Successfully Update profile information with Id: {userId}"
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
