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
    public class RoleController : ControllerBase
    {
        private IAspRoleData _aspRoleData;
        private readonly ILogger<RoleController> _logger;
        public RoleController(IAspRoleData aspRoleData, ILogger<RoleController> logger)
        {
            _aspRoleData = aspRoleData;
            _logger = logger;
        }

        [HttpGet]
        [Route("allroles")]
        public IActionResult GetRoles()
        {
            try
            {
                var roleList = _aspRoleData.GetRoles();
                // return Ok(_aspRoleData.GetRoles());
                return Ok(roleList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpGet]
        [Route("roles/{id}")]
        public IActionResult GetRole(string id)
        {
            try
            {
                var role = _aspRoleData.GetRoleByCustId(id);
                if (role != null)
                {
                    return Ok(role);
                }
                return NotFound("Role Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }
    }
}
