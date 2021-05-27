using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("Booking")]
    public partial class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BookingDate { get; set; }
        public int StatuId { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        [Column(TypeName = "date")]
        public DateTime ReservedDate { get; set; }
        [Column(TypeName = "time(2)")]
        public TimeSpan ReservedTime { get; set; }

        [ForeignKey(nameof(StatuId))]
        [InverseProperty(nameof(Status.Bookings))]
        public virtual Status Statu { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUser.Bookings))]
        public virtual AspNetUser User { get; set; }
        [InverseProperty(nameof(BookingDetail.Booking))]
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
