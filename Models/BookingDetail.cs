using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    public partial class BookingDetail
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public int? RestaurantId { get; set; }
        [Key]
        public int BookingId { get; set; }

        [ForeignKey(nameof(BookingId))]
        [InverseProperty("BookingDetails")]
        public virtual Booking Booking { get; set; }
        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty("BookingDetails")]
        public virtual Restaurant Restaurant { get; set; }
    }
}
