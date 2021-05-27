using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("TimeSlot")]
    public partial class TimeSlot
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "time(2)")]
        public TimeSpan AvailableTime { get; set; }
        public int TimeStatusId { get; set; }
        public int RestaurantId { get; set; }
        public int? Vacancy { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty("TimeSlots")]
        public virtual Restaurant Restaurant { get; set; }
        [ForeignKey(nameof(TimeStatusId))]
        [InverseProperty("TimeSlots")]
        public virtual TimeStatus TimeStatus { get; set; }
    }
}
