using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantBookingApi.Models;
using RestaurantBookingApi.RestaurantData;

namespace RestaurantBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantBookingDbContext _context;
        private readonly IRestaurant _restaurantData;
        private readonly ILogger<RestaurantsController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RestaurantsController(RestaurantBookingDbContext context, IRestaurant restaurant, 
            ILogger<RestaurantsController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _restaurantData = restaurant;
            _logger = logger;

            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: api/Restaurants
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurants()
        {
            //return await _context.Restaurants.ToListAsync();
            try
            {
                var allRestaurant = _restaurantData.GetRestaurants();
                if (allRestaurant == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No Restaurant to be found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No Restaurant found." });
                }
                return Ok(allRestaurant);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"Some Internal Error Happened : {ex.Message}" });
            }
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurant(int? id)
        {
            try
            {
                var restaurant = _restaurantData.GetRestaurant(id);
                if (restaurant == null)
                {
                    _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No Restaurant with id {id} to be found.");
                    return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No such Restaurant with id : {id}" });
                }
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  IActionResult PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                _logger.LogWarning($"{StatusCodes.Status400BadRequest}, id and the object id is not matched");
                return BadRequest(new Response { Type = $"{StatusCodes.Status400BadRequest}", Message = $"Bad Request Detected" });
            }

            var existRestaurant = _restaurantData.GetRestaurant(id);
            if (existRestaurant == null)
            {
                _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No Restaurant with id {id} to be found.");
                return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No Restaurant Found" });
            }

            try
            {
                _restaurantData.EditRestaurant(restaurant);
                return Ok(new Response { 
                    Type =$"{StatusCodes.Status200OK}",
                    Message = $"Successfully Update Restaurant Details"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostRestaurant(NewRestaurantForm restaurant)
        {
            var existUser = await userManager.FindByIdAsync(restaurant.UserId);
            if(existUser == null)
            {
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No user found."
                });
            }

            var x = _context.AspNetRoles.Where(x => x.Name == "Master").FirstOrDefault();
            var xRole = _context.AspNetUserRoles.Where(u => u.UserId == existUser.Id).Include(r => r.Role).FirstOrDefault();

            if (xRole.Role.Name != "Admin")
            {
                var removeUser = await userManager.RemoveFromRoleAsync(existUser, UserRoles.User);
                if (!removeUser.Succeeded)
                {
                    return Conflict(new Response
                    {
                        Type = $"Error",
                        Message = $"Erroor removing user from roles"
                    });
                }

                var UpgradeUser = await userManager.AddToRoleAsync(existUser, UserRoles.Admin);
                if (!UpgradeUser.Succeeded)
                {
                    return Conflict(new Response
                    {
                        Type = $"{UpgradeUser.GetType()}",
                        Message = $"{UpgradeUser.ToString()}"
                    });
                }
            }


            try
            {
                var AddRestaurant = _restaurantData.AddRestaurant(restaurant);
                if (AddRestaurant == null)
                {
                    _logger.LogWarning("Some error happen while trying to add new restaurant");
                    return Conflict(new Response { Type = $"{StatusCodes.Status409Conflict}", Message = $"Conflict Adding New Restaurant" });
                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Sucessfully Approved New Restaurant"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { 
                    Type = $"{ex.GetType()}", 
                    Message = $"{ex.Message}" 
                });
            }
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            var existRestaurant = _restaurantData.GetRestaurant(id);
            if (existRestaurant == null)
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
                _restaurantData.DeleteRestaurant(existRestaurant);
                return Ok(new Response { Type = $"Sucess", Message = $"Restaurant : {existRestaurant.Name} Deleted " });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }
    }
}
