using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("Status")]
    public partial class Status
    {
        public Status()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column("Status")]
        [StringLength(50)]
        public string Status1 { get; set; }

        [InverseProperty(nameof(Booking.Statu))]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
