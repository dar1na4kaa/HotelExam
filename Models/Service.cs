namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            BookingServices = new HashSet<BookingService>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceID { get; set; }

        public int BookingID { get; set; }

        [Required]
        [StringLength(150)]
        public string ServiceName { get; set; }

        [Required]
        [StringLength(150)]
        public string ServiceType { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Booking Booking { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingService> BookingServices { get; set; }
    }
}
