using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("NewRestaurantForm")]
    public partial class NewRestaurantForm
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string RestaurantName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int? Approval { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
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
    }
}
