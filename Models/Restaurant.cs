using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("Restaurant")]
    public partial class Restaurant
    {
        public Restaurant()
        {
            BookingDetails = new HashSet<BookingDetail>();
            Tables = new HashSet<Table>();
            TimeSlots = new HashSet<TimeSlot>();
            UserProfiles = new HashSet<UserProfile>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("Restaurant")]
        public virtual Address Address { get; set; }
        [InverseProperty(nameof(BookingDetail.Restaurant))]
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
        [InverseProperty(nameof(Table.Restaurant))]
        public virtual ICollection<Table> Tables { get; set; }
        [InverseProperty(nameof(TimeSlot.Restaurant))]
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
        [InverseProperty(nameof(UserProfile.Restaurant))]
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
