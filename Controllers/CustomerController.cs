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
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerData _customerData;

        public CustomerController(ILogger<CustomerController> logger, ICustomerData customerData)
        {
            _logger = logger;
            _customerData = customerData;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetCustomers()
        {
            try
            {
                var c = _customerData.GetCustomers();
                if (c != null)
                {
                    return Ok(c);
                }
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no user found");
                return NotFound("No user Found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get all user exception error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get/{custNo}")]
        public IActionResult GetCustomer(string custNo)
        {
            try
            {
                var c = _customerData.GetCustomer(custNo);
                return c != null ? Ok(c) : (IActionResult)NotFound("No Customer Found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get single user exception error : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getByUser/{userName}")]
        public IActionResult GetCustomerByUsername(string userName)
        {
            try
            {
                var c = _customerData.GetCustomerByUsername(userName);
                return c != null ? Ok(c) : (IActionResult)NotFound("No Customer Found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get single user exception error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("edit/{custNo}")]
        public IActionResult EditCustomer(string custNo, AspNetUser customer)
        {
            try
            {
                var existCustomer = _customerData.GetCustomer(custNo);
                if (existCustomer != null)
                {
                    customer.Id = existCustomer.Id;
                    _customerData.EditCustomer(customer);
                    return Ok(_customerData.GetCustomer(customer.Id));
                }
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no user found to edited.");
                return NotFound("No Customer Found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Edit user exception error : {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("delete/{custNo}")]
        public IActionResult DeleteCustomer(string custNo)
        {
            try
            {
                var existCustomer = _customerData.GetCustomer(custNo);
                if (existCustomer != null)
                {
                    _customerData.DeleteCustomer(existCustomer);
                    return Ok("Customer Account Deleted");
                }
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, no user found to be deleted");
                return NotFound("No Customer Found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete user exception error : {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
