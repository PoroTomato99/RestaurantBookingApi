using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("TimeStatus")]
    public partial class TimeStatus
    {
        public TimeStatus()
        {
            TimeSlots = new HashSet<TimeSlot>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column("TimeStatus")]
        [StringLength(50)]
        public string TimeStatus1 { get; set; }

        [InverseProperty(nameof(TimeSlot.TimeStatus))]
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }
}
