using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Keyless]
    [Table("DateSlot")]
    public partial class DateSlot
    {
        public int Id { get; set; }
        [Column("DateSlot", TypeName = "date")]
        public DateTime DateSlot1 { get; set; }
    }
}
