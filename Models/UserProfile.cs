using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RestaurantBookingApi.Models
{
    [Table("UserProfile")]
    public partial class UserProfile
    {
        [Key]
        public string Id { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateJoined { get; set; }
        public int? RestaurantId { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(AspNetUser.UserProfile))]
        public virtual AspNetUser IdNavigation { get; set; }
        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty("UserProfiles")]
        public virtual Restaurant Restaurant { get; set; }
    }
}
