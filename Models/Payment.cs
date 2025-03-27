namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        public int PaymentID { get; set; }

        public int BookingID { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PaymentDate { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentMethod { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
