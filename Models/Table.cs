using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("Table")]
    public partial class Table
    {
        [Key]
        public int Id { get; set; }
        public int TableNum { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty("Tables")]
        public virtual Restaurant Restaurant { get; set; }
    }
}
