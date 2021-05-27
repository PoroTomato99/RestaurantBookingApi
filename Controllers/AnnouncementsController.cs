using Microsoft.AspNetCore.Mvc;
using System;
using RestaurantBookingApi.Models;
using EasyNetQ;
using Microsoft.AspNetCore.Http;
using RestaurantBookingApi.RestaurantData;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly Models.RestaurantBookingDbContext _dbContext;
        private readonly IAnnouncement _announcement;
        private readonly ILogger<AnnouncementsController> _logger;

        public AnnouncementsController(Models.RestaurantBookingDbContext dbContext, IAnnouncement announcement, ILogger<AnnouncementsController> logger)
        {
            _dbContext = dbContext;
            _announcement = announcement;
            _logger = logger;
        }

        // GET api/<AnnouncementsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AnnouncementsController>
        [HttpPost]
        public IActionResult Post(RestaurantClassLibrary.Annoucement annoucement)
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            try
            {
                annoucement.Date = DateTime.Now;
                bus.PubSub.Publish(new RestaurantClassLibrary.Annoucement { Date = annoucement.Date, Type = annoucement.Type, Message = annoucement.Message });
                return Ok(new Response { Type = "Success", Message = "Notification published to queue." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}.\n Error Message:Failed to publish notification to queue: {ex.Message}");
                return BadRequest(new Response { Type = $"{StatusCodes.Status400BadRequest}", Message = $"Error Message: {ex.Message}" });
            }
        }
    }
}
