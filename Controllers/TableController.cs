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
    public class TableController : ControllerBase
    {
        private readonly ITable _tableData;
        private readonly ILogger<TableController> _logger;


        public TableController(ITable table, ILogger<TableController> logger)
        {
            _tableData = table;
            _logger = logger;
        }

        //Get All Table into a list
        [HttpGet]
        public IActionResult OnGet()
        {
            try
            {
                var x = _tableData.GetTables();
                return Ok(x);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

    //Get Table by Id
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTable(int? id)
        {
            if(id == null)
            {
                return Conflict(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No table id provided" });
            }

            try
            {
                var x = _tableData.GetTable(id);
                return Ok(x);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        //Add New Table
        [HttpPost]
        public IActionResult AddTable(Table table)
        {
            var isTableExist = _tableData.IsTableExist(table);
            if(table == null)
            {
                return Conflict(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"Table information were missing." });
            }

            try
            {
                var x = _tableData.AddTable(table);
                if(x == null)
                {
                    _logger.LogWarning("Some error happen while trying to add new restaurant");
                    return Conflict(new Response { Type = $"{StatusCodes.Status409Conflict}", Message = $"Conflict Adding New Restaurant" });
                }
                return Ok(x);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        //Delete Table
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTable(int? id)
        {
            if(id == null)
            {
                return Conflict(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"Table information were missing." });
            }
            var table = _tableData.GetTable(id);
            if(table == null)
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
                _tableData.DeleteTable(table);
                return Ok(new Response { Type = $"{StatusCodes.Status200OK}", Message = $"Successfully Deleted Table from Database" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
                return Conflict(new Response { Type = $"{ex.GetType()}", Message = $"{ex.Message}" });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult PutTable(int? id, Table table)
        {
            if(id == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, Information to get query table semms to be missing.");
                return Conflict(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"Table information were missing." });
            }

            if(id != table.Id)
            {
                _logger.LogWarning($"{StatusCodes.Status400BadRequest}, Information about table is being tampered.");
                return BadRequest(new Response { Type = $"{StatusCodes.Status400BadRequest}", Message = $"Information might have been altered." });
            }

            var xTable = _tableData.GetTable(id);
            if(xTable == null)
            {
                _logger.LogWarning($"Type : {StatusCodes.Status404NotFound}, Error Message : No table with id {id} to be found.");
                return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No table Found" });
            }

            try
            {
               var x =  _tableData.EditTable(table);
                if(x == null)
                {
                    return Conflict(new Response
                    {
                        Type = $"{StatusCodes.Status500InternalServerError}",
                        Message = $"There are some errors occured while attempting to update table information."
                    });               
                }
                //return Ok(_tableData.GetTable(id));
                return Ok(new Response {
                    Type = $"{StatusCodes.Status200OK}", 
                    Message = $"Successfully Update table with Id: {id}" 
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
                                                               