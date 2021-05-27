using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("Address")]
    public partial class Address
    {
        [Key]
        public int RestaurantId { get; set; }
        public int? BuildingNo { get; set; }
        [StringLength(100)]
        public string Address1 { get; set; }
        [StringLength(100)]
        public string Address2 { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(100)]
        public string PostCode { get; set; }
        [StringLength(100)]
        public string Country { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty("Address")]
        public virtual Restaurant Restaurant { get; set; }
    }
}
