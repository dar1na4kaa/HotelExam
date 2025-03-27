namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            Bookings = new HashSet<Booking>();
            Cleaning_Schedule = new HashSet<Cleaning_Schedule>();
        }

        public int RoomID { get; set; }

        [Required]
        [StringLength(50)]
        public string Floor { get; set; }

        public int RoomNumber { get; set; }

        public int TypeId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cleaning_Schedule> Cleaning_Schedule { get; set; }

        public virtual RoomType RoomType { get; set; }
    }
}
