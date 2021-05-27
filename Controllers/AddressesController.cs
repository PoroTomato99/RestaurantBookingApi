using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantBookingApi.Models;

namespace RestaurantBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly RestaurantBookingDbContext _context;

        public AddressesController(RestaurantBookingDbContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return null;
                //return NotFound(new Response
                //{
                //    Type = $"{StatusCodes.Status404NotFound}",
                //    Message = $"No address found."
                //});
            }
            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.RestaurantId)
            {
                return BadRequest(
                    new Response
                    {
                        Type = $"{StatusCodes.Status400BadRequest}",
                        Message = $"Database error occurred while updating address."
                    });
            }

            //_context.Entry(address).State = EntityState.Modified;
            var x = _context.Addresses.Find(id);
            if(x == null)
            {
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No address found with Id : {id}"
                });
            }

            x.BuildingNo = address.BuildingNo;
            x.Address1 = address.Address1;
            x.Address2 = address.Address2;
            x.State = address.State;
            x.PostCode = address.PostCode;
            x.Country = address.Country;
            _context.Addresses.Update(x);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new Response { Type = $"{StatusCodes.Status200OK}", Message = $"Successfull updated address." });
            }
            catch (Exception ex)
            {
                    return BadRequest( new Response
                    {
                        Type = $"{ex.GetType()}",
                        Message = $"{ex.Message}"
                    });
            }
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostAddress(Address address)
        {
            if (AddressExists(address.RestaurantId))
            {
                var x = new DuplicateNameException();
                return BadRequest(new Response
                {
                    Type = $"{x.GetType()}",
                    Message = $"Address already existed."
                });
            }
            _context.Addresses.Add(address);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Type = $"{ex.GetType()}",
                    Message = $"{ex.Message}"
                });
            }
            return Ok(new Response
            {
                Type = $"{StatusCodes.Status200OK}",
                Message = $"Successfully added address."
            });
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No address found!"
                });
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return Ok(new Response
            {
                Type = $"{StatusCodes.Status200OK}",
                Message = $"Successfully deleted address."
            });
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.RestaurantId == id);
        }
    }
}
