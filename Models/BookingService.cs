namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BookingService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookingServiceID { get; set; }

        public int BookingID { get; set; }

        public int ServiceID { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Service Service { get; set; }
    }
}
