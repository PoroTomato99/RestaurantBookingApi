using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    public partial class Annoucement
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(500)]
        public string Message { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
    }
}
